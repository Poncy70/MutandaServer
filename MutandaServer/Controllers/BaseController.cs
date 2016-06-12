using System.Web.Http.Controllers;
using System.Security.Claims;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Security.Principal;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public abstract class BaseController<TData> : TableController<TData> where TData : class, ITableData
    {
        protected bool mIsAutenticated;
        protected ProviderCredentials mCredentials;
        protected ConnectionInfo mConnectionInfo;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            OrderEntryNetContext context = new OrderEntryNetContext();
            DomainManager = new EntityDomainManager<TData>(context, Request);
        }

        protected virtual async Task InitializeBaseAsync(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var claimsPrincipal = this.User as ClaimsPrincipal;

            mIsAutenticated = claimsPrincipal.Identity.IsAuthenticated;

            if (mIsAutenticated)
            {
                mCredentials = null;

                string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                string provider = claimsPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;

                switch (provider.ToLower())
                {
                    case "google":
                        Task<GoogleCredentials> mAutenticateTask;
                        mAutenticateTask = User.GetAppServiceIdentityAsync<GoogleCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTask);

                        if (mAutenticateTask.IsCompleted)
                        {
                            mCredentials = mAutenticateTask.Result;
                            mConnectionInfo = ControllerStatic.GetDBSource(mCredentials);
                        }

                        break;

                    case "microsoftaccount":
                        Task<MicrosoftAccountCredentials> mAutenticateTaskMicrosoft;
                        mAutenticateTaskMicrosoft = User.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskMicrosoft);

                        if (mAutenticateTaskMicrosoft.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskMicrosoft.Result;
                            mConnectionInfo = ControllerStatic.GetDBSource(mCredentials);
                        }
                        break;

                    case "facebook":
                        Task<FacebookCredentials> mAutenticateTaskFacebook;
                        mAutenticateTaskFacebook = User.GetAppServiceIdentityAsync<FacebookCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskFacebook);

                        if (mAutenticateTaskFacebook.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskFacebook.Result;
                            mConnectionInfo = ControllerStatic.GetDBSource(mCredentials);
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task RunSafe(Task task, bool notifyOnError = true, [CallerMemberName] string caller = "")
        {
            Exception exception = null;

            try
            {
                await Task.Run(() =>
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                        task.Wait();
                });
            }
            catch (TaskCanceledException)
            {

            }
            catch (AggregateException e)
            {
                var ex = e.InnerException;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                exception = ex;
            }
            catch (Exception e)
            {
                exception = e;
            }
        }

        protected string MakeConnectionString()
        {
            return string.Format(@"data source=tcp:{0};initial catalog={1};persist security info=True;user id={2}; password={3}; MultipleActiveResultSets=True", mConnectionInfo.ServerName, mConnectionInfo.DBName, mConnectionInfo.DBUser, mConnectionInfo.DBPassword);
        }
    }

    public class ConnectionInfo
    {
        public string DBName { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string ServerName { get; set; }
        public string DeviceMail { get; set; }
        public int IdAgente { get; set; }
        public bool SuperUser { get; set; }
    }
}
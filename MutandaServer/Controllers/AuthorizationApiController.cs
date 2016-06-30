using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Security.Claims;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Runtime.CompilerServices;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using OrderEntry.Net.Models;
using System.Data;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public class AuthorizationApiController : ApiController
    {
        private bool mIsAutenticated;
        private ProviderCredentials mCredentials;
        private string mUserId;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private MobileServiceAuthenticationProvider mAuthProvider;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();
            
            if (initializeBase.IsCompleted)
            {
            }
        }

        private async Task InitializeBaseAsync(HttpControllerContext controllerContext)
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
                        Task<GoogleCredentials> mAutenticateTaskGoogle;
                        mAutenticateTaskGoogle = User.GetAppServiceIdentityAsync<GoogleCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskGoogle);

                        if (mAutenticateTaskGoogle.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskGoogle.Result;
                            mUserId = mCredentials.UserId;
                            mAuthProvider = MobileServiceAuthenticationProvider.Google;
                        }

                        break;

                    case "microsoftaccount":
                        Task<MicrosoftAccountCredentials> mAutenticateTaskMicrosoft;
                        mAutenticateTaskMicrosoft = User.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskMicrosoft);

                        if (mAutenticateTaskMicrosoft.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskMicrosoft.Result;
                            mAuthProvider = MobileServiceAuthenticationProvider.MicrosoftAccount;
                        }

                        break;

                    case "facebook":
                        Task<FacebookCredentials> mAutenticateTaskFacebook;
                        mAutenticateTaskFacebook = User.GetAppServiceIdentityAsync<FacebookCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskFacebook);

                        if (mAutenticateTaskFacebook.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskFacebook.Result;
                            mAuthProvider = MobileServiceAuthenticationProvider.Facebook;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        private async Task<JToken> GetProviderInfo(string url)
        {
            var c = new HttpClient();
            var resp = await c.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return JToken.Parse(await resp.Content.ReadAsStringAsync());
        }

        public async Task<Authorization> Post([FromBody] GEST_Ordini_Teste ordine, string idDevice)
        {
            ConnectionInfo connectionInfo = ControllerStatic.GetDBSource(mCredentials);

            Authorization authorizationModel = new Authorization();

            if (connectionInfo.DBName != null && connectionInfo.DBName != "")
            {
                DBData db = new DBData(connectionInfo);
                string sql = "SELECT DeviceMail, IdAgente FROM DEVICE_ParametriDevice WHERE IdDevice = '" + idDevice + "'";
                DataTable dt = db.ReadData(sql);

                string deviceMail = "";
                int idAgente = 0;

                if (dt.Rows.Count > 0)
                {
                    deviceMail = (string)dt.Rows[0]["DeviceMail"];
                    idAgente = (int)dt.Rows[0]["IdAgente"];
                }

                authorizationModel.DBName = connectionInfo.DBName;
                authorizationModel.DeviceMail = deviceMail;
                authorizationModel.IdAgente = idAgente;
                authorizationModel.SuperUser = connectionInfo.SuperUser;
                authorizationModel.OAuthProvider = (short)mAuthProvider;
                authorizationModel.AccesDenied = false;

                if (idAgente == 0)
                    ControllerStatic.WriteErrorLog(connectionInfo, "Authorization.Post", idDevice);
            }
            else
                authorizationModel.AccesDenied = true;

            return authorizationModel;
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
    }
}

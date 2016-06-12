using System.Linq;
using OrderEntry.Net.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server.Config;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public class AuthorizationController : BaseController<Authorization>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<Authorization>(context, Request);
            }
        }

        public IQueryable<Authorization> GetAllPermission()
        {
            IQueryable<Authorization> i = null;

            try
            {
                Authorization firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "AuthorizationModelController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<Authorization> GetPermission(string id)
        {
            return Lookup(id);
        }

        public Task<Authorization> PatchPermission(string id, Delta<Authorization> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostPermission(Authorization item)
        {
            Authorization current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeletePermission(string id)
        {
            return DeleteAsync(id);
        }
    }
}
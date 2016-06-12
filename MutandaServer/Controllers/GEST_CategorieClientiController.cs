using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public class GEST_CategorieClientiController : BaseController<GEST_CategorieClienti>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_CategorieClienti>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_CategorieClienti> GetAllGEST_CategorieClienti()
        {
            IQueryable<GEST_CategorieClienti> i = null;

            try
            {
                GEST_CategorieClienti firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_CategorieClientiController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_CategorieClienti> GetGEST_CategorieClienti(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_CategorieClienti> PatchGEST_CategorieClienti(string id, Delta<GEST_CategorieClienti> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_CategorieClienti(GEST_CategorieClienti item)
        {
            GEST_CategorieClienti current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_CategorieClienti(string id)
        {
            return DeleteAsync(id);
        }
    }
}
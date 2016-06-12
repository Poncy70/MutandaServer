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
    public class GEST_ListiniController : BaseController<GEST_Listini>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Listini>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Listini> GetAllGEST_Listini()
        {
            IQueryable<GEST_Listini> i = null;

            try
            {
                GEST_Listini firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_ListiniController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Listini> GetGEST_Listini(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Listini> PatchGEST_Listini(string id, Delta<GEST_Listini> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Listini(GEST_Listini item)
        {
            GEST_Listini current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Listini(string id)
        {
            return DeleteAsync(id);
        }
    }
}
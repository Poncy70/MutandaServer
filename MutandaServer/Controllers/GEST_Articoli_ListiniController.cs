using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Articoli_ListiniController : BaseController<GEST_Articoli_Listini>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Articoli_Listini>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Articoli_Listini> GetAllGEST_Articoli_Listini()
        {
            IQueryable<GEST_Articoli_Listini> i = null;

            try
            {
                GEST_Articoli_Listini firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Articoli_ListiniController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Articoli_Listini> GetGEST_Articoli_Listini(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Articoli_Listini> PatchGEST_Articoli_Listini(string id, Delta<GEST_Articoli_Listini> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Articoli_Listini(GEST_Articoli_Listini item)
        {
            GEST_Articoli_Listini current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Articoli_Listini(string id)
        {
            return DeleteAsync(id);
        }
    }
}
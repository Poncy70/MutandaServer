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
    public class GEST_Articoli_ClassiController : BaseController<GEST_Articoli_Classi>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Articoli_Classi>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Articoli_Classi> GetAllGEST_Articoli_Classi()
        {
            IQueryable<GEST_Articoli_Classi> i = null;

            try
            {
                GEST_Articoli_Classi firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Articoli_ClassiController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Articoli_Classi> GetGEST_Articoli_Classi(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Articoli_Classi> PatchGEST_Articoli_Classi(string id, Delta<GEST_Articoli_Classi> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Articoli_Classi(GEST_Articoli_Classi item)
        {
            GEST_Articoli_Classi current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Articoli_Classi(string id)
        {
            return DeleteAsync(id);
        }
    }
}
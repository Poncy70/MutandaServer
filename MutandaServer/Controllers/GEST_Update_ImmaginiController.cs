using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Update_ImmaginiController : BaseController<GEST_Update_Immagini>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Update_Immagini>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Update_Immagini> GetAllGEST_Update_Immagini()
        {
            IQueryable<GEST_Update_Immagini> i = null;

            try
            {
                GEST_Update_Immagini firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Update_ImmaginiController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Update_Immagini> GetGEST_Update_Immagini(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Update_Immagini> PatchGEST_Update_Immagini(string id, Delta<GEST_Update_Immagini> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Update_Immagini(GEST_Update_Immagini item)
        {
            GEST_Update_Immagini current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Update_Immagini(string id)
        {
            return DeleteAsync(id);
        }
    }
}
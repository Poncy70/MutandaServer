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
    public class GEST_PortoController : BaseController<GEST_Porto>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Porto>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Porto> GetAllGEST_Iva()
        {
            IQueryable<GEST_Porto> i = null;

            try
            {
                GEST_Porto firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_PortoController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Porto> GetGEST_Iva(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Porto> PatchGEST_Iva(string id, Delta<GEST_Porto> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Iva(GEST_Porto item)
        {
            GEST_Porto current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Iva(string id)
        {
            return DeleteAsync(id);
        }
    }
}
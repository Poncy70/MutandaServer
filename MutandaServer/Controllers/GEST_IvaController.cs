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
    public class GEST_IvaController : BaseController<GEST_Iva>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Iva>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Iva> GetAllGEST_Iva()
        {
            IQueryable<GEST_Iva> i = null;

            try
            {
                GEST_Iva firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_IvaController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Iva> GetGEST_Iva(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Iva> PatchGEST_Iva(string id, Delta<GEST_Iva> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Iva(GEST_Iva item)
        {
            GEST_Iva current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Iva(string id)
        {
            return DeleteAsync(id);
        }
    }
}
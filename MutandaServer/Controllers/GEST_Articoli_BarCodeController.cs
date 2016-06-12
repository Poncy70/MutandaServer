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
    public class GEST_Articoli_BarcodeController : BaseController<GEST_Articoli_BarCode>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Articoli_BarCode>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Articoli_BarCode> GetAllGEST_Articoli_Barcode()
        {
            IQueryable<GEST_Articoli_BarCode> i = null;

            try
            {
                GEST_Articoli_BarCode firstElement;
                i = Query();

                if (i.Count() > 0) 
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Articoli_BarcodeController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Articoli_BarCode> GetGEST_Articoli_Barcode(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Articoli_BarCode> PatchGEST_Articoli_Barcode(string id, Delta<GEST_Articoli_BarCode> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Articoli_Barcode(GEST_Articoli_BarCode item)
        {
            GEST_Articoli_BarCode current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Articoli_Barcode(string id)
        {
            return DeleteAsync(id);
        }
    }
}
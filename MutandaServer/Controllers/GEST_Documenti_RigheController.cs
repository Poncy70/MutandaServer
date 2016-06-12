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
    public class GEST_Documenti_RigheController : BaseController<GEST_Documenti_Righe>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Documenti_Righe>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Documenti_Righe> GetAllGEST_Documenti_Righe()
        {
            IQueryable<GEST_Documenti_Righe> i = null;

            try
            {
                GEST_Documenti_Righe firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Documenti_RigheController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Documenti_Righe> GetGEST_Documenti_Righe(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Documenti_Righe> PatchGEST_Documenti_Righe(string id, Delta<GEST_Documenti_Righe> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Documenti_Righe(GEST_Documenti_Righe item)
        {
            GEST_Documenti_Righe current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Documenti_Righe(string id)
        {
            return DeleteAsync(id);
        }
    }
}
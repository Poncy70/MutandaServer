using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Documenti_TesteController : BaseController<GEST_Documenti_Teste>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Documenti_Teste>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Documenti_Teste> GetAllGEST_Documenti_Teste()
        {
            IQueryable<GEST_Documenti_Teste> i = null;

            try
            {
                GEST_Documenti_Teste firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Documenti_TesteController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Documenti_Teste> GetGEST_Documenti_Teste(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Documenti_Teste> PatchGEST_Documenti_Teste(string id, Delta<GEST_Documenti_Teste> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Documenti_Teste(GEST_Documenti_Teste item)
        {
            GEST_Documenti_Teste current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Documenti_Teste(string id)
        {
            return DeleteAsync(id);
        }
    }
}
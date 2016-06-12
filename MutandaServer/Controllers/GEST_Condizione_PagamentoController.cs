using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Condizione_PagamentoController : BaseController<GEST_Condizione_Pagamento>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Condizione_Pagamento>(context, Request);
            }
        }

        public IQueryable<GEST_Condizione_Pagamento> GetAllGEST_Condizione_Pagamento()
        {
            IQueryable<GEST_Condizione_Pagamento> i = null;

            try
            {
                GEST_Condizione_Pagamento firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Condizione_PagamentoController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Condizione_Pagamento> GetGEST_Condizione_Pagamento(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Condizione_Pagamento> PatchGEST_Condizione_Pagamento(string id, Delta<GEST_Condizione_Pagamento> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Condizione_Pagamento(GEST_Condizione_Pagamento item)
        {
            GEST_Condizione_Pagamento current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Condizione_Pagamento(string id)
        {
            return DeleteAsync(id);
        }
    }
}
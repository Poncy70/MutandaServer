using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Scala_ScontiController : BaseController<GEST_Scala_Sconti>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Scala_Sconti>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Scala_Sconti> GetAllGEST_Scala_Sconti()
        {
            IQueryable<GEST_Scala_Sconti> i = null;

            try
            {
                GEST_Scala_Sconti firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Scala_ScontiController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Scala_Sconti> GetGEST_Scala_Sconti(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Scala_Sconti> PatchGEST_Scala_Sconti(string id, Delta<GEST_Scala_Sconti> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Scala_Sconti(GEST_Scala_Sconti item)
        {
            GEST_Scala_Sconti current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Scala_Sconti(string id)
        {
            return DeleteAsync(id);
        }
    }
}
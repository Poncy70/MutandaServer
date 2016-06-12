using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_UnitaMisuraController : BaseController<GEST_UnitaMisura>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_UnitaMisura>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_UnitaMisura> GetAllGEST_UnitaMisura()
        {
            IQueryable<GEST_UnitaMisura> i = null;

            try
            {
                GEST_UnitaMisura firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_UnitaMisuraController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_UnitaMisura> GetGEST_UnitaMisura(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_UnitaMisura> PatchGEST_UnitaMisura(string id, Delta<GEST_UnitaMisura> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_UnitaMisura(GEST_UnitaMisura item)
        {
            GEST_UnitaMisura current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_UnitaMisura(string id)
        {
            return DeleteAsync(id);
        }
    }
}
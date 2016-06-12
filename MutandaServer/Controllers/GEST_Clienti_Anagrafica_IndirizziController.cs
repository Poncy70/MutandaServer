using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;
using System.Collections.Generic;

namespace OrderEntry.Net.Service
{
    [Authorize]
    public class GEST_Clienti_Anagrafica_IndirizziController : BaseController<GEST_Clienti_Anagrafica_Indirizzi>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Clienti_Anagrafica_Indirizzi>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Clienti_Anagrafica_Indirizzi> GetAllGEST_Anagrafica_Indirizzi()
        {
            IQueryable<GEST_Clienti_Anagrafica_Indirizzi> i = null;

            try
            {
                GEST_Clienti_Anagrafica_Indirizzi firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Clienti_Anagrafica_IndirizziController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Clienti_Anagrafica_Indirizzi> GetGEST_Anagrafica_Indirizzi(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Clienti_Anagrafica_Indirizzi> PatchGEST_Anagrafica_Indirizzi(string id, Delta<GEST_Clienti_Anagrafica_Indirizzi> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Anagrafica_Indirizzi(GEST_Clienti_Anagrafica_Indirizzi item)
        {
            GEST_Clienti_Anagrafica_Indirizzi current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Anagrafica_Indirizzi(string id)
        {
            return DeleteAsync(id);
        }
    }
}
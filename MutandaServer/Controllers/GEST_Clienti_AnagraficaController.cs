using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server.Config;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    [Authorize]
    public class GEST_Clienti_AnagraficaController : BaseController<GEST_Clienti_Anagrafica>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Clienti_Anagrafica>(context, Request, enableSoftDelete: true);
            }
        }

        //protected override IQueryable<GEST_Clienti_Anagrafica> Query()
        //{
        //    IQueryable<GEST_Clienti_Anagrafica> clientiQuery = null;
        //    IEnumerable<GEST_Clienti_Anagrafica> forn = (from clienti in context.GEST_Clienti_Anagrafica
        //                                                    where (clienti.IdAgente == mConnectionInfo.IdAgente ||
        //                                                        clienti.IdAgente2 == mConnectionInfo.IdAgente) //|| mConnectionInfo.SuperUser == true
        //                                                    select clienti);
                                           

        //    clientiQuery = forn.AsQueryable();
        //    return clientiQuery;
        //}

        public IQueryable<GEST_Clienti_Anagrafica> GetAllGEST_Clienti_Anagrafica()
        {
            IQueryable<GEST_Clienti_Anagrafica> i = null;

            try
            {
                GEST_Clienti_Anagrafica firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Clienti_AnagraficaController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Clienti_Anagrafica> GetGEST_Clienti_Anagrafica(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Clienti_Anagrafica> PatchGEST_Clienti_Anagrafica(string id, Delta<GEST_Clienti_Anagrafica> patch)
        {
            try
            {
                return UpdateAsync(id, patch);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Clienti_Anagrafica_TesteController.PostGEST_Ordini_Teste", e, "");
            }

            return null;
        }

        public async Task<IHttpActionResult> PostGEST_Clienti_Anagrafica(GEST_Clienti_Anagrafica item)
        {
            try
            {
                GEST_Clienti_Anagrafica current = await InsertAsync(item);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            catch (HttpResponseException re)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Clienti_Anagrafica_TesteController.PostGEST_Ordini_Teste", re, re.Response.ReasonPhrase);
                return ResponseMessage(re.Response);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Clienti_Anagrafica_TesteController.PostGEST_Ordini_Teste", e, "");
            }

            return null;
        }

        public Task DeleteGEST_Clienti_Anagrafica(string id)
        {
            return DeleteAsync(id);
        }
    }
}
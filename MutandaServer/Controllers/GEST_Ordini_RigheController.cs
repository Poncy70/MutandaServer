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
    public class GEST_Ordini_RigheController : BaseController<GEST_Ordini_Righe>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Ordini_Righe>(context, Request, enableSoftDelete: true);
            }
        }

        // GET tables/GEST_Ordini_Righe/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<GEST_Ordini_Righe> GetGEST_Ordini_Righe(string id)
        {
            return Lookup(id);
        }

        protected override IQueryable<GEST_Ordini_Righe> Query()
        {
            IQueryable<GEST_Ordini_Righe> righeQuery = null;
            IEnumerable<GEST_Ordini_Righe> righe = (from righeOrdini in context.GEST_Ordini_Righe
                                                    join testeOrdini in context.GEST_Ordini_Teste.Where(a => a.IdAgente == mConnectionInfo.IdAgente)
                                                         on righeOrdini.IdSlave equals testeOrdini.Id
                                                    into VGroup2
                                                    from VRigheOrdini in VGroup2
                                                    select righeOrdini);

            righeQuery = righe.AsQueryable();
            return righeQuery;
        }

        // GET tables/GEST_Ordini_Righe
        public IQueryable<GEST_Ordini_Righe> GetAllGEST_Ordini_Righe()
        {             
            IQueryable<GEST_Ordini_Righe> i = null;

            try
            {
                GEST_Ordini_Righe firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_RigheController", e, i.ToString());
            }

            return null;
        }

        // PATCH tables/GEST_Ordini_Righe/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<GEST_Ordini_Righe> PatchGEST_Ordini_Righe(string id, Delta<GEST_Ordini_Righe> patch)
        {
            try
            {
                return UpdateAsync(id, patch);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_RigheController", e, "");
            }

            return null;
        }

        // POST tables/GEST_Ordini_Righe
        public async Task<IHttpActionResult> PostGEST_Ordini_Righe(GEST_Ordini_Righe item)
        {
            try
            {
                if (!ExistOrdine(item.Id))
                {
                    item.CloudState = 0;
                    GEST_Ordini_Righe current = await InsertAsync(item);
                    return CreatedAtRoute("Tables", new { id = current.Id }, current);
                }
                else
                    return CreatedAtRoute("Tables", new { id = item.Id }, item);
            }
            catch (HttpResponseException re)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_RigheController", re, re.Response.ReasonPhrase);
                return ResponseMessage(re.Response);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_RigheController", e, "");
            }

            return null;
        }

        // DELETE tables/GEST_Ordini_Righe/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteGEST_Ordini_Righe(string id)
        {
            return DeleteAsync(id);
        }

        private bool ExistOrdine(string id)
        {
            return context.GEST_Ordini_Righe.Where(a => a.Id == id).Count() > 0;
        }
    }
}
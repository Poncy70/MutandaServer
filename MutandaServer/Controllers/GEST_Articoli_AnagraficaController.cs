﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class GEST_Articoli_AnagraficaController : BaseController<GEST_Articoli_Anagrafica>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Articoli_Anagrafica>(context, Request, enableSoftDelete: true);
            }
        }

        public IQueryable<GEST_Articoli_Anagrafica> GetAllGEST_Articoli_Anagrafica()
        {
            IQueryable<GEST_Articoli_Anagrafica> i = null;

            try
            {
                GEST_Articoli_Anagrafica firstElement;
                i = Query();

                if (i.Count() > 0 )
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Articoli_AnagraficaController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Articoli_Anagrafica> GetGEST_Articoli_Anagrafica(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Articoli_Anagrafica> PatchGEST_Articoli_Anagrafica(string id, Delta<GEST_Articoli_Anagrafica> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_Articoli_Anagrafica(GEST_Articoli_Anagrafica item)
        {
            GEST_Articoli_Anagrafica current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_Articoli_Anagrafica(string id)
        {
            return DeleteAsync(id);
        }
    }
}
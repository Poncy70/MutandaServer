﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Data;

namespace OrderEntry.Net.Service
{
    public class GEST_Ordini_TesteController : BaseController<GEST_Ordini_Teste>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                this.Configuration.Formatters.JsonFormatter.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<GEST_Ordini_Teste>(context, Request, enableSoftDelete: true);
            }
        }

        protected override IQueryable<GEST_Ordini_Teste> Query()
        {
            IQueryable<GEST_Ordini_Teste> testeQuery = null;
            IEnumerable<GEST_Ordini_Teste> teste = (from testeOrdini in context.GEST_Ordini_Teste
                                                    where testeOrdini.IdAgente == mConnectionInfo.IdAgente //|| mConnectionInfo.SuperUser == true
                                                    select testeOrdini);

            testeQuery = teste.AsQueryable();
            return testeQuery;
        }

        public IQueryable<GEST_Ordini_Teste> GetAllGEST_Ordini_Teste()
        {
            IQueryable<GEST_Ordini_Teste> i = null;

            try
            {
                GEST_Ordini_Teste firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_TesteController.PatchGEST_Ordini_Teste", e, i.ToString());
            }

            return null;
        }

        public SingleResult<GEST_Ordini_Teste> GetGEST_Ordini_Teste(string id)
        {
            return Lookup(id);
        }

        public Task<GEST_Ordini_Teste> PatchGEST_Ordini_Teste(string id, Delta<GEST_Ordini_Teste> patch)
        {
            try
            {
                return UpdateAsync(id, patch);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_TesteController.PatchGEST_Ordini_Teste", e, "");
            }

            return null;
        }

        public async Task<IHttpActionResult> PostGEST_Ordini_Teste(GEST_Ordini_Teste item)
        {
            try
            {
                item.NumeroOrdineDevice = GetNumeroOrdine(item.DeviceMail);
                item.NumeroOrdineGenerale = GetNumeroOrdineGenerale();
                item.CloudState = 0;

                GEST_Ordini_Teste current = await InsertAsync(item);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            catch (HttpResponseException re)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_TesteController.PostGEST_Ordini_Teste", re, re.Response.ReasonPhrase);
                return ResponseMessage(re.Response);
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_Ordini_TesteController.PostGEST_Ordini_Teste", e, "");
            }

            return null;
        }

        public Task DeleteGEST_Ordini_Teste(string id)
        {
            return DeleteAsync(id);
        }

        private string GetNumeroOrdine(string deviceMail)
        {
            ConnectionInfo connectionInfo = ControllerStatic.GetDBSource(mCredentials);
            DBData db = new DBData(connectionInfo);

            string sql = string.Empty;
            string numeroOrdine;
            string prefisso = string.Empty;
            string suffisso = string.Empty;

            try
            {
                sql = string.Format("SELECT IsNull(PrefissoNumerazione, ''), IsNull(SuffissoNumerazione, '') FROM DEVICE_ParametriDevice WHERE DeviceMail = '{0}'", deviceMail);
                DataTable dtParam = db.ReadData(sql);

                if (dtParam.Rows.Count > 0)
                {
                    prefisso = (string)dtParam.Rows[0]["PrefissoNumerazione"];
                    suffisso = (string)dtParam.Rows[0]["SuffissoNumerazione"];
                }

                sql = string.Format("SELECT ISNULL(MAX(CAST(SUBSTRING(NumeroOrdineDevice, {0}, 7) as int)),0) As MaxOrdine FROM GEST_Ordini_Teste WHERE DeviceMail = '{1}'", prefisso.Length + 1, deviceMail);
                DataTable dt = db.ReadData(sql);

                if (dt.Rows.Count > 0)
                    if (dt.Rows[0]["MaxOrdine"] != null)
                        numeroOrdine = ((int)dt.Rows[0]["MaxOrdine"] + 1).ToString().PadLeft(7, '0');
                    else
                        numeroOrdine = "1".PadLeft(7, '0');
                else
                    numeroOrdine = "1".PadLeft(7, '0');

                if (prefisso != "")
                    numeroOrdine = prefisso + numeroOrdine;

                if (suffisso != "")
                    numeroOrdine = numeroOrdine + suffisso;
            }
            catch (Exception ex)
            {
                ControllerStatic.WriteErrorLog(connectionInfo, "GEST_Ordini_TesteController.GetNumeroOrdine", ex, sql);
                return "";
            }

            return numeroOrdine;
        }

        private int GetNumeroOrdineGenerale()
        {
            ConnectionInfo connectionInfo = ControllerStatic.GetDBSource(mCredentials);
            DBData db = new DBData(connectionInfo);
            int numeroOrdine = 1;

            string sql = string.Format("SELECT ISNULL(MAX(NumeroOrdineGenerale) + 1,0) As MaxOrdine FROM GEST_Ordini_Teste");

            try
            {
                DataTable dt = db.ReadData(sql);
                if (dt.Rows.Count > 0) numeroOrdine = (int)dt.Rows[0]["MaxOrdine"];
            }
            catch (Exception ex)
            {
                ControllerStatic.WriteErrorLog(connectionInfo, "GEST_Ordini_TesteController.GetNumeroOrdineGenerale", ex, sql);
            }

            return numeroOrdine;
        }
    }
}
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Security.Claims;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Runtime.CompilerServices;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using OrderEntry.Net.Models;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public class OrdiniSaveApiController : ApiController
    {
        private bool mIsAutenticated;
        private ProviderCredentials mCredentials;
        private string mUserId;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private MobileServiceAuthenticationProvider mAuthProvider;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
            }
        }

        private async Task InitializeBaseAsync(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var claimsPrincipal = this.User as ClaimsPrincipal;

            mIsAutenticated = claimsPrincipal.Identity.IsAuthenticated;

            if (mIsAutenticated)
            {
                mCredentials = null;

                string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                string provider = claimsPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;

                switch (provider.ToLower())
                {
                    case "google":
                        Task<GoogleCredentials> mAutenticateTaskGoogle;
                        mAutenticateTaskGoogle = User.GetAppServiceIdentityAsync<GoogleCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskGoogle);

                        if (mAutenticateTaskGoogle.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskGoogle.Result;
                            mUserId = mCredentials.UserId;
                            mAuthProvider = MobileServiceAuthenticationProvider.Google;
                        }

                        break;

                    case "microsoftaccount":
                        Task<MicrosoftAccountCredentials> mAutenticateTaskMicrosoft;
                        mAutenticateTaskMicrosoft = User.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskMicrosoft);

                        if (mAutenticateTaskMicrosoft.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskMicrosoft.Result;
                            mAuthProvider = MobileServiceAuthenticationProvider.MicrosoftAccount;
                        }

                        break;

                    case "facebook":
                        Task<FacebookCredentials> mAutenticateTaskFacebook;
                        mAutenticateTaskFacebook = User.GetAppServiceIdentityAsync<FacebookCredentials>(controllerContext.Request);
                        await RunSafe(mAutenticateTaskFacebook);

                        if (mAutenticateTaskFacebook.IsCompleted)
                        {
                            mCredentials = mAutenticateTaskFacebook.Result;
                            mAuthProvider = MobileServiceAuthenticationProvider.Facebook;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        private async Task<JToken> GetProviderInfo(string url)
        {
            var c = new HttpClient();
            var resp = await c.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return JToken.Parse(await resp.Content.ReadAsStringAsync());
        }

        public PushResult Post([FromBody] GEST_Ordini_Teste ordine, string action)
        {
            PushResult result = null;

            switch (action)
            {
                case "Added":
                    result = Insert(ordine);
                    break;

                default:
                    break;
            }

            return result;
        }

        private PushResult Insert(GEST_Ordini_Teste ordine)
        {
            PushResult result = null;

            StringBuilder sqlTesta = new StringBuilder();
            StringBuilder sqlRiga = new StringBuilder();
            List<string> sqlRighe = new List<string>();

            ConnectionInfo connectionInfo = ControllerStatic.GetDBSource(mCredentials);
            DBData db = new DBData(connectionInfo);

            try
            {
                string numeroOrdineDevice = GetNumeroOrdine(ordine.DeviceMail);

                if (numeroOrdineDevice != string.Empty)
                {
                    sqlTesta.Append("INSERT INTO GEST_Ordini_Teste(Id, RagioneSociale, PartitaIva, CodiceFiscale, DataDocumento, NumeroOrdineDevice, ");
                    sqlTesta.Append("CodPagamento, CodListino, TotaleDocumento, CloudState, DataConsegna, TotaleConsegna, IdAgente, IdAnagrafica, Note, ");
                    sqlTesta.Append("IdIndSpedMerce, RagSocSped, IndirizzoSped, CittaSped, CapSped, ProvSped, NazioneSped, NrRigheTot, DeviceMail, IdDevice, UpdatedAt) ");
                    sqlTesta.Append("VALUES(");
                    sqlTesta.AppendFormat("'{0}',", ordine.Id);
                    sqlTesta.AppendFormat("'{0}',", ordine.RagioneSociale);
                    sqlTesta.AppendFormat("'{0}',", ordine.PartitaIva);
                    sqlTesta.AppendFormat("'{0}',", ordine.CodiceFiscale);
                    sqlTesta.AppendFormat("'{0}',", ordine.DataDocumento.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                    sqlTesta.AppendFormat("'{0}',", numeroOrdineDevice);
                    sqlTesta.AppendFormat("'{0}',", ordine.CodPagamento);
                    sqlTesta.AppendFormat("'{0}',", ordine.CodListino);
                    sqlTesta.AppendFormat("{0},", ordine.TotaleDocumento);
                    sqlTesta.AppendFormat("{0},", ordine.CloudState);

                    if (ordine.DataConsegna.HasValue)
                        sqlTesta.AppendFormat("'{0}',", ordine.DataConsegna.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                    else
                        sqlTesta.AppendFormat("{0},", "Null");

                    sqlTesta.AppendFormat("{0},", ordine.TotaleConsegna);
                    sqlTesta.AppendFormat("{0},", ordine.IdAgente);
                    sqlTesta.AppendFormat("{0},", ordine.IdAnagrafica);
                    sqlTesta.AppendFormat("'{0}',", ordine.Note);
                    sqlTesta.AppendFormat("{0},", ordine.IdIndSpedMerce);
                    sqlTesta.AppendFormat("'{0}',", ordine.RagSocSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.IndirizzoSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.CittaSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.CapSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.ProvSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.NazioneSped);
                    sqlTesta.AppendFormat("'{0}',", ordine.NrRigheTot);
                    sqlTesta.AppendFormat("'{0}',", ordine.DeviceMail);
                    sqlTesta.AppendFormat("'{0}',", ordine.IdDevice);
                    sqlTesta.Append("getDate()");
                    sqlTesta.Append(")");

                    if (ordine.RigheOrdine.Count > 0)
                    {
                        string guiRiga;

                        foreach (GEST_Ordini_Righe riga in ordine.RigheOrdine)
                        {
                            guiRiga = Guid.NewGuid().ToString();

                            sqlRiga.Clear();
                            sqlRiga.Append("INSERT INTO GEST_Ordini_Righe(Id, IdSlave, IdRiga, TipoRiga, CodArt, Descrizione, Qta, NCP_QtaScontoMerce, ");
                            sqlRiga.Append("CodUnMis, CodIva, ValUnit, Sc1, Sc2, Sc3, Sc4, ImportoSconto, DataPresuntaConsegna, Imponibile, Imposta, Totale, UpdatedAt) ");
                            sqlRiga.Append("VALUES(");
                            sqlRiga.AppendFormat("'{0}',", riga.Id);
                            sqlRiga.AppendFormat("'{0}',", ordine.Id);
                            sqlRiga.AppendFormat("'{0}',", riga.IdRiga);
                            sqlRiga.AppendFormat("{0},", riga.TipoRiga);
                            sqlRiga.AppendFormat("'{0}',", riga.CodArt);
                            sqlRiga.AppendFormat("'{0}',", riga.Descrizione);
                            sqlRiga.AppendFormat("{0},", riga.Qta);
                            sqlRiga.AppendFormat("{0},", riga.NCP_QtaScontoMerce);
                            sqlRiga.AppendFormat("'{0}',", riga.CodUnMis);
                            sqlRiga.AppendFormat("'{0}',", riga.CodIva);
                            sqlRiga.AppendFormat("{0},", riga.ValUnit);
                            sqlRiga.AppendFormat("{0},", riga.Sc1);
                            sqlRiga.AppendFormat("{0},", riga.Sc2);
                            sqlRiga.AppendFormat("{0},", riga.Sc3);
                            sqlRiga.AppendFormat("{0},", riga.Sc4);
                            sqlRiga.AppendFormat("{0},", riga.ImportoSconto);

                            if (riga.DataPresuntaConsegna.HasValue)
                                sqlRiga.AppendFormat("'{0}',", riga.DataPresuntaConsegna.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                            else
                                sqlRiga.AppendFormat("{0},", "Null");

                            sqlRiga.AppendFormat("{0},", riga.Imponibile);
                            sqlRiga.AppendFormat("{0},", riga.Imposta);
                            sqlRiga.AppendFormat("{0},", riga.Totale);
                            sqlRiga.Append("getDate()");
                            sqlRiga.Append(")");

                            sqlRighe.Add(sqlRiga.ToString());
                        }
                    }

                    db.BeginTransaction();
                    db.Execute(sqlTesta.ToString());

                    foreach (string riga in sqlRighe)
                        db.Execute(riga);

                    db.CommitTransaction();

                    result = new PushResult() { Message = "Inserimento Effettuato.", NumeroOrdineDevice = numeroOrdineDevice, OK = true };
                }
                else
                    result = new PushResult() { Message = "Inserimento fallito. Impossibile attribuire un numero d'ordine", NumeroOrdineDevice = "", OK = false };
            }
            catch (Exception ex)
            {
                if (db.TransactionOn)
                    db.RollbackTransaction();

                ControllerStatic.WriteErrorLog(connectionInfo, "OrdiniSaveController", ex, "TESTA: " + sqlTesta.ToString() + " RIGHE: " + sqlRiga.ToString());

                result = new PushResult() { Message = "Inserimento fallito.", NumeroOrdineDevice = "", OK = false };
            }

            return result;
        }

        private async Task RunSafe(Task task, bool notifyOnError = true, [CallerMemberName] string caller = "")
        {
            Exception exception = null;

            try
            {
                await Task.Run(() =>
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                        task.Wait();
                });
            }
            catch (TaskCanceledException)
            {

            }
            catch (AggregateException e)
            {
                var ex = e.InnerException;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                exception = ex;
            }
            catch (Exception e)
            {
                exception = e;
            }
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
                sql = string.Format("SELECT PrefissoNumerazione, SuffissoNumerazione FROM DEVICE_ParametriDevice WHERE DeviceMail = '{0}'", deviceMail);
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
                ControllerStatic.WriteErrorLog(connectionInfo, "OrdiniSaveController", ex, sql);
                return "";
            }

            return numeroOrdine;
        }
    }
}

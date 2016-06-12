using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Documenti_Teste : EntityData
    {
        public GEST_Documenti_Teste()
        {           
        }

        public int IdDoc { get; set; }
        public int IdAnagrafica { get; set; }
        public int TipoDocumento { get; set; }
        public DateTime ? DataDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodPagamento { get; set; }
        public string CodListino { get; set; }
        public string CodDivisa { get; set; }
        public int StatoChiusura { get; set; }
        public int StatoOrdine { get; set; }
        public decimal TotaleMerce { get; set; }
        public decimal TotOmaggi { get; set; }
        public decimal TotNonImponibile { get; set; }
        public decimal TotImposta { get; set; }
        public decimal TotaleDocumento { get; set; }
        public bool AddebitaBolli { get; set; }
        public bool AddebitaIncasso { get; set; }
        public bool AccontoInCassa { get; set; }
        public decimal SpeseBolli { get; set; }
        public decimal SpeseIncasso { get; set; }
        public decimal SpeseSped { get; set; }
        public decimal SpeseImballo { get; set; }
        public decimal SpeseVarie { get; set; }
        public decimal SpeseAcc { get; set; }
        public decimal Acconto { get; set; }
        public decimal Abbuono { get; set; }
        public decimal Contrassegno { get; set; }
        public decimal PercSconto { get; set; }
        public decimal Sconto { get; set; }
        public decimal PercMaggiorazione { get; set; }
        public decimal Maggiorazione { get; set; }
        public int IdIndSpedizione { get; set; }
        public DateTime ? DataPresConsegna { get; set; }
        public int IdAgente { get; set; }
    }
}

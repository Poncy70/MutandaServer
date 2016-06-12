using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Listini : EntityData
    {
        public GEST_Listini()
        {
        }
        
        public string CodListino { get; set; }
        public short TipoListino { get; set; }        
        public string Descrizione { get; set; }
        public bool ConIva { get; set; }
        public DateTime ? DataIniz { get; set; }
        public DateTime? DataFin { get; set; }
        public string CodCatListino { get; set; }
        public string CodDivisa { get; set; }
        public DateTime UltVariazione { get; set; }
        public bool ConvertiInDivisaDoc { get; set; }
        public decimal PercMaggValUnit { get; set; }
        public decimal FinoAImporto1 { get; set; }
        public decimal FinoAImporto2 { get; set; }
        public decimal FinoAImporto3 { get; set; }
        public decimal FinoAImporto4 { get; set; }
        public decimal FinoAImporto5 { get; set; }
        public decimal FinoAImporto6 { get; set; }
        public decimal FinoAImporto7 { get; set; }
        public short NrDec1 { get; set; }
        public short NrDec2 { get; set; }
        public short NrDec3 { get; set; }
        public short NrDec4 { get; set; }
        public short NrDec5 { get; set; }
        public short NrDec6 { get; set; }
        public short NrDec7 { get; set; }
        public bool FlagArrotFisso { get; set; }
        public short NrDec { get; set; }
    }
}


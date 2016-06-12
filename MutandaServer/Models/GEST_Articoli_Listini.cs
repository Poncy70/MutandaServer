using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_Listini : EntityData
    {
        public GEST_Articoli_Listini()
        {
        }

        public string CodListino { get; set; }
        public string CodArt { get; set; }
        public decimal Importo { get; set; }
        public decimal PercSconto1 { get; set; }
        public decimal PercSconto2 { get; set; }
        public decimal PercSconto3 { get; set; }
        public decimal PercSconto4 { get; set; }
        public decimal ImportoSconto { get; set; }
        public bool UsaScalaSconti { get; set; }
        public DateTime UltVariazione { get; set; }
        public string CodUnMis { get; set; }
        public short TipoRiga { get; set; }
        public string TipoElemento { get; set; }
    }
}


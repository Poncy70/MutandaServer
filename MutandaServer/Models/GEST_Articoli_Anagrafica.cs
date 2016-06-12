using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_Anagrafica : EntityData
    {
        public GEST_Articoli_Anagrafica()
        {
        }

        public string CodArt { get; set; }
        public string Descrizione { get; set; }
        public short TipoArticolo { get; set; }
        public string CodUnMisBase { get; set; }
        public string CodUnMisVend { get; set; }
        public decimal PrezzoVendita { get; set; }
        public decimal PercSconto1 { get; set; }
        public decimal PercSconto2 { get; set; }
        public decimal PercSconto3 { get; set; }
        public decimal PercSconto4 { get; set; }
        public string CodIva { get; set; }
        public string CodMerc { get; set; }
        public string CodStat { get; set; }
        public string CodFamiglia { get; set; }
        public string CodClasse { get; set; }
        public string CodNatura { get; set; }
        public string CodMarca { get; set; }
        public string CodModello { get; set; }
        public string CodCatScontoArt { get; set; }
        public string CodCatProvvArt { get; set; }
        public DateTime ? FineValidita { get; set; }
        public bool Lotto { get; set; }
        public bool ArticoloMatricolato { get; set; }
        public bool ArticoloVendibile { get; set; }
    }
}

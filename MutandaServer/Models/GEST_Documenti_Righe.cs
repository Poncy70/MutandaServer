using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Documenti_Righe : EntityData
    {
        public GEST_Documenti_Righe()
        {
            
        }
        public string IdSlave { get; set; }
        public int IdDoc { get; set; }
        public int IdRiga { get; set; }
        public string CodArt { get; set; }
        public string Descrizione { get; set; }
        public decimal Qta { get; set; }
        public string CodUnMis { get; set; }
        public string CodIva { get; set; }
        public decimal ValoreUnitario { get; set; }
        public decimal Sconto1 { get; set; }
        public decimal Sconto2 { get; set; }
        public decimal Sconto3 { get; set; }
        public decimal Sconto4 { get; set; }
        public decimal Imponibile { get; set; }
        public DateTime ? DataPresuntaConsegna { get; set; }
        public decimal ImportoSconto { get; set; }
        public decimal Imposta { get; set; }
        public decimal Totale { get; set; }
    }
}

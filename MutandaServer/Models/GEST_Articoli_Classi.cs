using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_Classi : EntityData
    {
        public GEST_Articoli_Classi()
        {
        }
        
        public string CodClasse { get; set; }
        public string Descrizione { get; set; }
        public string CodFamiglia { get; set; }
        public string Icona { get; set; }
        public int Ordinamento { get; set; }
    }
}


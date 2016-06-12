using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_Immagini : EntityData
    {
        public GEST_Articoli_Immagini()
        {
        }

        public string CodArt { get; set; }
        public string NomeFile { get; set; }
        public bool FotoDefault { get; set; }
    }
}


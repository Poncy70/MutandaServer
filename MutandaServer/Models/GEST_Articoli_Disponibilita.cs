using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_Disponibilita : EntityData
    {
        public GEST_Articoli_Disponibilita()
        {
        }
        
        public string CodDep { get; set; }
        public string CodArt { get; set; }
        public decimal QtaDisponibile { get; set; }
        public decimal QtaDisponibileSuArrivi { get; set; }        
    }
}


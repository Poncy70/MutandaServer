using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Iva : EntityData
    {
        public GEST_Iva()
        {
        }
        
        public string CodIva { get; set; }
        public string Descrizione { get; set; }
        public decimal PercIva { get; set; }
    }
}


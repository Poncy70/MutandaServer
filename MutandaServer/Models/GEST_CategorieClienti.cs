using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_CategorieClienti : EntityData
    {
        public GEST_CategorieClienti()
        {
        }
        
        public string CodCatCliente { get; set; }
        public string Descrizione { get; set; }
    }
}


using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_UnitaMisura : EntityData
    {
        public GEST_UnitaMisura()
        {
        }
        
        public string CodUnMis { get; set; }
        public string Descrizione { get; set; }        
    }
}


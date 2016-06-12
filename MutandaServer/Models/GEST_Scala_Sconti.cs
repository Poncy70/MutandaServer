using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Scala_Sconti : EntityData
    {
        public GEST_Scala_Sconti()
        {
            CodListino = CodArticolo = string.Empty;
        }
        
        public string CodListino { get; set; }
        public string CodArticolo { get; set; }
        public int IdRiga { get; set; }
        public decimal QtaDa { get; set; }
        public decimal QtaA { get; set; }
        public decimal ValUnitario { get; set; }
        public decimal PercSconto1 { get; set; }
        public decimal PercSconto2 { get; set; }
        public decimal PercSconto3 { get; set; }
        public decimal PercSconto4 { get; set; }
        public decimal ImportoSconto { get; set; }
    }
}


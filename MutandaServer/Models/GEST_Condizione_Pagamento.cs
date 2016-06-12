using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Condizione_Pagamento : EntityData
    {
        public GEST_Condizione_Pagamento()
        {
        }
        
        public string CodPagamento { get; set; }
        public string Descrizione { get; set; }
        public string NCP_DescrizioneWeb { get; set; }
    }
}


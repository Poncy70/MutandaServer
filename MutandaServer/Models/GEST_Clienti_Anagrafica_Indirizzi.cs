using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Clienti_Anagrafica_Indirizzi : EntityData
    {
        public GEST_Clienti_Anagrafica_Indirizzi()
        {
            RagioneSociale = string.Empty;
        }

        public int IDAnagrafica { get; set; }
        public int IdIndirizzo { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string IdAnagraficaDevice { get; set; }
        public short CloudState { get; set; }
    }
}

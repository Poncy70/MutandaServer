using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models 
{
    public class GEST_Clienti_Anagrafica : EntityData
    {
        public GEST_Clienti_Anagrafica()
        {
        }

        public int IDAnagrafica { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string PartitaIva { get; set; }
        public string CodiceFiscale { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string CodPagamento { get; set; }
        public string CodCatCliente { get; set; }
        public string CodListino { get; set; }
        public string CodPorto { get; set; }
        public string Note { get; set; }
        public bool ClienteAttivo { get; set; }
        public short CloudState { get; set; }
        public int IdAgente { get; set; }
        public int IdAgente2 { get; set; }
        public int IdIndSpedMerceDefault { get; set; }
        public string Iban { get; set; }
        public decimal PercSconto1 { get; set; }
        public decimal PercSconto2 { get; set; }
        public decimal PercSconto3 { get; set; }
        public decimal PercSconto4 { get; set; }
        public string RifInterno { get; set; }
    }
}

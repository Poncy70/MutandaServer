using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class GEST_Articoli_BarCode : EntityData
    {
        public GEST_Articoli_BarCode()
        {
            
            CodArt = TextCode=string.Empty;
            BarCodeType = 0;
            
        }
        
        public string CodArt { get; set; }
        public string TextCode { get; set; }
        public int BarCodeType { get; set; }
    }
}

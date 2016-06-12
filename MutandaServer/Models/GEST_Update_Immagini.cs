using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Update_Immagini : EntityData
    {
        public GEST_Update_Immagini()
        {
        }
        
        public string NomeFile { get; set; }
        public string PathFile { get; set; }
        public DateTime Data { get; set; }
        public string TipoImmagine { get; set; }
    }
}


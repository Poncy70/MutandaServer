using Microsoft.Azure.Mobile.Server;

namespace OrderEntry.Net.Models
{
    public class Authorization: EntityData
    {
        public string DeviceMail { get; set; }
        public string DBName { get; set; }
        public int IdAgente { get; set; }
        public bool SuperUser { get; set; }
        public short OAuthProvider { get; set; }
        public bool AccesDenied { get; set; }
    }
}
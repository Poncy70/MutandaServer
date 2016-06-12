using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OrderEntry.Net.Service.Startup))]

namespace OrderEntry.Net.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
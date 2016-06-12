using System.Configuration;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using AutoMapper;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);


            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<GEST_Ordini_TesteDTO, GEST_Ordini_Teste>()
            //        .ForMember(gest_Ordini_TesteDTO => gest_Ordini_TesteDTO.RigheOrdine,
            //                            map => map.MapFrom(gest_Ordini_Teste => gest_Ordini_Teste.RigheOrdine));
            //    cfg.CreateMap<GEST_Ordini_TesteDTO, GEST_Ordini_Teste>()
            //            .ForMember(gest_Ordini_Teste => gest_Ordini_Teste.RigheOrdine,
            //                        map => map.MapFrom(gest_Ordini_TesteDTO => gest_Ordini_TesteDTO.RigheOrdine));

            //    cfg.CreateMap<GEST_Ordini_Righe, GEST_Ordini_RigheDTO>();
            //    cfg.CreateMap<GEST_Ordini_RigheDTO, GEST_Ordini_Righe>();
            //});

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }
}


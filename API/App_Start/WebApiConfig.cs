using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                 name: "NsoupFilter",
                routeTemplate: "api/Nsoup/{filter}",
                defaults: new { controller = "Nsoup", action = "GetFilter", filter=" "}
                );

            config.Routes.MapHttpRoute(
              name: "NsoupGetData",
             routeTemplate: "api/Nsoup/GetDataMagazine/{url}",
             defaults: new { controller = "Nsoup", action = "GetDataMagazine", url = " " }
             );

            config.Routes.MapHttpRoute(
              name: "MagazineByIdUser",
             routeTemplate: "api/Magazines/User/{idUser}",
             defaults: new { controller = "Magazines", action = "GetMagazinesByIdUser", idUser = " " }
             );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}

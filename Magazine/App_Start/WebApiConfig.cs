using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Magazine
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FilterNsoup",
                routeTemplate: "Home/Index/{filter:string}",
                defaults: new { controller = "Home", action = "Index" }
            );

            config.Routes.MapHttpRoute(
             name: "AddMagazine",
             routeTemplate: "MagazineDiary/AddDiary/{url:string}",
             defaults: new { controller = "MagazinesDiary", action = "AddDiary" }
         );
            //config.Routes.MapHttpRoute(
            //    name: "SaveMagazines",
            //    routeTemplate: "Home/Save/{lstMagazines}",
            //    defaults: new { controller = "Home", action = "Save" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

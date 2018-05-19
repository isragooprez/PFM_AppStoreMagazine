using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Magazine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default2",
            //    url: "Nsoup/{filter}",
            //    defaults: new { controller = "Nsoup", action = "ListNsoupFilter" }
            //);

           // routes.MapRoute(
           //    name: "Defaultfilter",
           //    url: "Home/Index/{filterNsoupMagazine}",
           //    defaults: new { controller = "Home", action = "Index"}
           //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.SaleMedia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "Rss",
           //    url: "rss.xml",
           //    defaults: new { controller = "Rss", action = "CreateRss", id = UrlParameter.Optional }
           //);
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TTS", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //Kanske använder denna, sparar så länge!
            routes.MapRoute(
                name: "Test",
                url: "Forum/SelectedPost/{id}",
                defaults: new { controller = "Forum", action = "SelectedPost", id = UrlParameter.Optional });
        }
    }
}

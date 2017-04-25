using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace social_network
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DisplayPosts",
                url: "Posts",
                defaults: new { controller = "Posts", action = "Index" }
            );

            routes.MapRoute(
                name: "DisplayUsers",
                url: "Users",
                defaults: new { controller="Users", action="Index" }
            );

            routes.MapRoute(
                name: "SearchUsers",
                url: "Users/Search",
                defaults: new { controller = "Users", action = "Search" }
            );

            routes.MapRoute(
                name: "DisplayUser",
                url: "Users/{id}",
                defaults: new { controller = "Users", action = "Show" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

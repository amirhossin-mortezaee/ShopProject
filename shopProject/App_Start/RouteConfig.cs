using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shopProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "shopProject.Controllers" }
            );
            routes.MapRoute(
                name: "adminRoutes",
                url: "{controller}/{action}/{id}",
                defaults: new { area="Admin",controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "shopProject.Areas.Admin.Controllers" }
            ).DataTokens.Add("area","Admin");
            routes.MapRoute(
                name: "UserPanel",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "UserPanel", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "shopProject.Areas.User.Controllers" }
            ).DataTokens.Add("area", "UserPanel");
        }
    }
}

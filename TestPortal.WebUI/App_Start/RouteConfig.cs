using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestPortal.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                null,
                "",// Соответствует только пустому URL  (т.е.  /)
                new
                {
                    controller = "Home",
                    action = "Index",
                    category = (string)null,
                    page = 1
                }
            );   

            routes.MapRoute(
                name: "QuestionEditor",
                url: "{controller}/{action}",
                defaults: new { controller = "QuestionEditor", action = "Index" }
            );
        }
    }
}

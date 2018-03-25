using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace BookShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new {controller = "Product", action = "List", category=(string)null, id=1}
                );


            routes.MapRoute(
               name: null,
               url: "Strona{page}",
               defaults: new { Controller = "Product", Action = "List", category=(string)null },  
               constraints: new {page = @"\d+"}
               );

            routes.MapRoute(
                name: null,
                url: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1}
            );

            routes.MapRoute(
                name: null,
                url: "{category}/Strona{page}",
                defaults: new {controller = "Product", action = "List"},
                constraints: new {page = @"\d+"}
                );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}"
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Admin.Common.App_Start;

namespace EmailAdmin.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            CommonRouteConfig.RegisterRoutes(routes);

            routes.MapRoute(
               name: "TrackEmailVerFechas",
               url: "TrackEmail/VerFechas/{Id}",
               defaults: new { controller = "TrackEmail", action = "VerFechas", Id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "TrackLinkVerFechas",
               url: "TrackLink/VerFechas/{Id}",
               defaults: new { controller = "TrackLink", action = "VerFechas", Id = UrlParameter.Optional }
           );
        }
    }
}

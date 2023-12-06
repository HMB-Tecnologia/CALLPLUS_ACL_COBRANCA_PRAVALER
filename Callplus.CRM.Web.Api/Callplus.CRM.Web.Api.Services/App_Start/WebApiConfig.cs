using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Callplus.CRM.Web.Api.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Bluechip/{controller}");
        }
    }
}
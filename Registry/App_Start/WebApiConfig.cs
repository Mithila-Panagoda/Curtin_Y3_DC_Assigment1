using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Registry
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",
                defaults: new { ActionName = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "testing",
                routeTemplate: "api/{controller}/{ActionName}",
                defaults: new { ActionName = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}

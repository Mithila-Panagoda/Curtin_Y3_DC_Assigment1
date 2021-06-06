using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ServiceProvider
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "OneNumber",
                routeTemplate: "{controller}/{num}/{token}",
                defaults: new { num = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "TwoNumbers",
                routeTemplate: "{controller}/{num1}/{num2}/{token}",
                defaults: new { num1 = RouteParameter.Optional,num2=RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ThreeNumbers",
                routeTemplate: "{controller}/{num1}/{num2}/{num3}/{token}",
                defaults: new { id = RouteParameter.Optional }
            );
            //making webapi return json instead of xml
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}

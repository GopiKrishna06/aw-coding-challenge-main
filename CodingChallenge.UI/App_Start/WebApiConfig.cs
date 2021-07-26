using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CodingChallenge.UI
{
    public static class WebApiConfig    {        public static void Register(HttpConfiguration config)        {
            // webapi routes 
            config.MapHttpAttributeRoutes();
            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");            config.EnableCors(cors);            config.Routes.MapHttpRoute(                name: "DefaultApi",                routeTemplate: "api/{controller}/{id}",                defaults: new { id = RouteParameter.Optional }            );        }    }

}

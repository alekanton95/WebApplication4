﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication4
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

//            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
//= Newtonsoft.Json.ReferenceLoopHandling.Serialize;
//            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
//                 = Newtonsoft.Json.PreserveReferencesHandling.Objects;

//            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
//= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}

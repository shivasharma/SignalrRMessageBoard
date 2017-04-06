﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace CommentMessage
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormattter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormattter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "RepliesRoute",
               routeTemplate: "api/v1/topics/{topicid}/replies/{id}",
               defaults: new { controller = "replies", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "DeleteRepliesRoute",
               routeTemplate: "api/v1/topics/replies/",
               defaults: new { controller = "replies", id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/topics/{id}",
                defaults: new { controller = "topics", id = RouteParameter.Optional }
            );
        }
    }
}

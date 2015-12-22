using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProductApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PostApi",
                routeTemplate: "api/posts/{id}",
                defaults: new {controller="Posts",  id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
              name: "PostingApi",
              routeTemplate: "api/posts/{value}",
              defaults: new { controller = "Posts", value = RouteParameter.Optional }
          );
            
            config.Routes.MapHttpRoute(
                name: "PostingsApi",
                routeTemplate: "api/postings/{value}",
                defaults: new { controller = "Postings", value = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "PostingsIDApi",
                routeTemplate: "api/postings/{id}",
                defaults: new { controller = "Postings", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "SearchStringApi",
                routeTemplate: "api/searchstrings/{searchString}",
                defaults: new { controller = "SearchStrings", searchString = RouteParameter.Optional }
            );
        }
    }
}

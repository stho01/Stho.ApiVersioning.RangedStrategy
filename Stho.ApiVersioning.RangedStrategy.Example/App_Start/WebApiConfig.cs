using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Stho.ApiVersioning.RangedStrategy.Conventions;

namespace Stho.ApiVersioning.RangedStrategy.Example
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };
            config.MapHttpAttributeRoutes(constraintResolver);
            // Web API configuration and services
            config.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.Conventions.Add(new RangedVersioningControllerConvention(ApiVersionConfig.GetCurrentVersions()));
            });

            // Web API routes

            config.Routes.MapHttpRoute(
                "VersionedQueryString",
                "api/{controller}",
                defaults: null);

            //config.Routes.MapHttpRoute(
            //    "VersionedUrl",
            //    "api/v{apiVersion}/{controller}",
            //    defaults: null,
            //    constraints: new { apiVersion = new ApiVersionRouteConstraint() });
        }
    }
}

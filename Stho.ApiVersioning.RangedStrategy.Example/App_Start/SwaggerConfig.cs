using System.Web.Http;
using System.Web.Http.Description;
using CustomAttributeExample.Documentation;
using Microsoft.Web.Http.Description;
using Swashbuckle.Application;

namespace CustomAttributeExample
{
    public static class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiExplorer = config.AddCustomVersionedApiExplorer();

            config.EnableSwagger("{apiVersion}/swagger", swagger => {
                // If your API has multiple versions, use "MultipleApiVersions" instead of "SingleApiVersion".
                // In this case, you must provide a lambda that tells Swashbuckle which actions should be
                // included in the docs for a given API version. Like "SingleApiVersion", each call to "Version"
                // returns an "Info" builder so you can provide additional metadata per API version.
                swagger.MultipleApiVersions(VersionSupportResolver, builder => BuildInfo(builder, apiExplorer));

                swagger.OperationFilter<SwaggerDefaultValues>();
            })
            .EnableSwaggerUi(swaggerUi => swaggerUi.EnableDiscoveryUrlSelector());
        }

        static VersionedApiExplorer AddCustomVersionedApiExplorer(this HttpConfiguration config)
        {
            return config.AddVersionedApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        static bool VersionSupportResolver(ApiDescription description, string version)
        {
            return description.GetGroupName() == version;
        }

        static void BuildInfo(VersionInfoBuilder builder, VersionedApiExplorer explorer)
        {
            foreach (var group in explorer.ApiDescriptions)
            {
                var description = "Custom attribute example";

                if (group.IsDeprecated)
                {
                    description += " (Deprecated)";
                }

                builder.Version(group.Name, $"Custom attribute example - {group.ApiVersion}");
            }
        }
    }
}

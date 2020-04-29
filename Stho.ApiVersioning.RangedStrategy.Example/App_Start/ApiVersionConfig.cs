using CustomAttributeExample.Mvc.Versioning;
using Microsoft.Web.Http;

namespace CustomAttributeExample
{
    public static class ApiVersionConfig
    {
        public static readonly ApiVersion V1_0 = new ApiVersion(1, 0);
        public static readonly ApiVersion V2_0 = new ApiVersion(2, 0);
        public static readonly ApiVersion V2_1 = new ApiVersion(2, 1);
        public static readonly ApiVersion V5_0 = new ApiVersion(5, 0);

        public static ApiVersions GetCurrentVersions() => new ApiVersions
        {
            V1_0,
            V2_0,
            V2_1,
            V5_0,
        };
    }
}
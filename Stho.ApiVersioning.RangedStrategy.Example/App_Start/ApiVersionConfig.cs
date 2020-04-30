using Microsoft.Web.Http;

namespace Stho.ApiVersioning.RangedStrategy.Example
{
    public static class ApiVersionConfig
    {
        public static readonly ApiVersion V1_0 = new ApiVersion(1, 0);
        public static readonly ApiVersion V2_0 = new ApiVersion(2, 0);
        public static readonly ApiVersion V2_1 = new ApiVersion(2, 1);

        public static ApiVersions GetCurrentVersions() => new ApiVersions
        {
            V1_0,
            V2_0,
            V2_1
        };
    }
}
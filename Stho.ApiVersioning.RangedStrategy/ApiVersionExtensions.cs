using Microsoft.Web.Http;

namespace Stho.ApiVersioning.RangedStrategy
{
    public static class ApiVersionExtensions
    {
        public static bool IsAvailableIn(this ApiVersion version, ApiVersion from, ApiVersion to)
        {
            return version >= from && version <= to;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Http;

namespace CustomAttributeExample.Mvc.Versioning
{
    public static class ApiVersionExtensions
    {
        public static bool IsAvailableIn(this ApiVersion version, ApiVersion from, ApiVersion to)
        {
            return version >= from && version <= to;
        }
    }
}
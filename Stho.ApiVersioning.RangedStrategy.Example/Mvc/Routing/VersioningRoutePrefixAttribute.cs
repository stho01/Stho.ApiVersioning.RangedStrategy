using System.Web.Http;

namespace CustomAttributeExample.Mvc.Routing
{
    public class VersioningRoutePrefixAttribute : RoutePrefixAttribute
    {
        public VersioningRoutePrefixAttribute() : this("{controller}") { }
        public VersioningRoutePrefixAttribute(string path) : base($"api/v{{version:apiVersion}}/{path}") {}
    }
}
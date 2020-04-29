using System.Web.Mvc;

namespace Stho.ApiVersioning.RangedStrategy.Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

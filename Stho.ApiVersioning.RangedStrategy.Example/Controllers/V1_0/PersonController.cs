using System.Collections.Generic;
using System.Web.Http;
using CustomAttributeExample.Mvc.Routing;
using Stho.ApiVersioning.RangedStrategy.Annotations;

namespace CustomAttributeExample.Controllers.V1_0
{
    [VersioningRoutePrefix("person")]
    [IntroducedInApiVersion(1, 0)]
    public class PersonController : ApiController
    {
        [Route]
        [IntroducedInApiVersion(1, 0)]
        public IEnumerable<string> Get()
        {
            yield return "Sten";
            yield return "Marius";
            yield return "Ola";
            yield return "Conny";
        }
    }
}
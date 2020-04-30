using System.Collections.Generic;
using System.Web.Http;
using Stho.ApiVersioning.RangedStrategy.Annotations;
using Stho.ApiVersioning.RangedStrategy.Example.Mvc.Routing;

namespace Stho.ApiVersioning.RangedStrategy.Example.Controllers.V2_0
{
    [VersioningRoutePrefix("values")]
    [IntroducedInApiVersion(2, 0)]
    public class ValuesController : ApiController
    {
        // api/v2/values
        [IntroducedInApiVersion(2)]
        [Route]
        public IEnumerable<string> GetExactRoute() => Get();

        // api/values?api-version=2.0   <- NOTE: Query parameter must be set, will otherwise target default version.
        [IntroducedInApiVersion(2)]
        public IEnumerable<string> Get()
        {
            yield return "v2 - item #1";
            yield return "v2 - item #2";
        }

        // api/v2/values/all
        [Route("all")]
        [IntroducedInApiVersion(2, 0)]
        public IEnumerable<string> GetAllValues()
        {
            yield return "v2 - item #1";
            yield return "v2 - item #2";
        }

        // api/v2/values/overwrite
        [Route("overwrite")]
        [IntroducedInApiVersion(2)]
        public string GetOverwriteEndpoint()
        {
            return "v2";
        }

        // api/v2/values/NewFeature
        [Route("NewFeature")]
        [IntroducedInApiVersion(2, 1)]
        public IEnumerable<string> GetNewFeature()
        {
            yield return "v2.1 NewFeature - item #1";
            yield return "v2.1 NewFeature - item #2";
        }
    }
}
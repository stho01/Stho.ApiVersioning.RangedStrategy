﻿using System.Collections.Generic;
using System.Web.Http;
using CustomAttributeExample.Mvc.Routing;
using Stho.ApiVersioning.RangedStrategy.Annotations;

namespace CustomAttributeExample.Controllers.V2_0
{
    [VersioningRoutePrefix("values")]
    [IntroducedInApiVersion(2, 0)]
    public class ValuesController : ApiController
    {
        [Route("all")]
        [IntroducedInApiVersion(2,0)]
        public IEnumerable<string> GetAllValues()
        {
            yield return "v2 - item #1";
            yield return "v2 - item #2";
        }

        [Route("NewFeature")]
        [IntroducedInApiVersion(2, 1)]
        public IEnumerable<string> GetNewFeature()
        {
            yield return "v2.1 NewFeature - item #1";
            yield return "v2.1 NewFeature - item #2";
        }
    }
}
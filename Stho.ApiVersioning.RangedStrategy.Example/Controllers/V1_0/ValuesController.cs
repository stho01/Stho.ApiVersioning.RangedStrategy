using System;
using System.Collections.Generic;
using System.Web.Http;
using CustomAttributeExample.Mvc.Routing;
using Microsoft.Web.Http;
using Stho.ApiVersioning.RangedStrategy.Annotations;

namespace CustomAttributeExample.Controllers.V1_0
{
    [VersioningRoutePrefix("values")]
    [IntroducedInApiVersion(1)]
    public class ValuesController : ApiController
    {
        [Route]
        [IntroducedInApiVersion(1)]
        [RemovedInApiVersion(2)]
        public IEnumerable<string> Get()
        {
            yield return "v1 - item #1";
            yield return "v1 - item #2";
        }

        [Route("{index:int}")]
        [IntroducedInApiVersion(1)]
        [RemovedInApiVersion(2)]
        public string GetByIndex(int index)
        {
            switch (index)
            {
                case 1: return $"v1 - item #1";
                case 2: return $"v1 - item #2";

                default: throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
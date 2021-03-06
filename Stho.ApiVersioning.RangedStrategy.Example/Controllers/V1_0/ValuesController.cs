﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Stho.ApiVersioning.RangedStrategy.Annotations;
using Stho.ApiVersioning.RangedStrategy.Example.Mvc.Routing;

namespace Stho.ApiVersioning.RangedStrategy.Example.Controllers.V1_0
{
    [VersioningRoutePrefix("values")]
    [IntroducedInApiVersion(1)]
    public class ValuesController : ApiController
    {
        // api/values
        [IntroducedInApiVersion(1)]
        [RemovedInApiVersion(2)]
        public IEnumerable<string> Get()
        {
            yield return "v1 - item #1";
            yield return "v1 - item #2";
        }

        // api/v1/overwrite
        [Route("overwrite")]
        [IntroducedInApiVersion(1)]
        [RemovedInApiVersion(2)]
        public string GetOverwriteEndpoint()
        {
            return "v1";
        }

        // api/v1/RemovedInV2/{index:int}
        [Route("RemovedInV2/{index:int}")]
        [IntroducedInApiVersion(1)]
        [RemovedInApiVersion(2)]
        public string GetByIndex(int index)
        {
            switch (index)
            {
                case 1: return "v1 - item #1";
                case 2: return "v1 - item #2";

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
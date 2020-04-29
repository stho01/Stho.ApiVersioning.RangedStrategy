using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Http;

namespace CustomAttributeExample.Mvc.Versioning
{
    
    public abstract class VersionHolderAttribute : Attribute
    {
        protected VersionHolderAttribute(ApiVersion version) { Version = version; }
        public ApiVersion Version { get; }
    }
}
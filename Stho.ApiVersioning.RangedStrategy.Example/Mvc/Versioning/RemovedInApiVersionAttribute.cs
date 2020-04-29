using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Http;

namespace CustomAttributeExample.Mvc.Versioning
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RemovedInApiVersionAttribute : VersionHolderAttribute
    {
        public RemovedInApiVersionAttribute(int major) : this(major, 0) { }
        public RemovedInApiVersionAttribute(int major, int minor) : this(new ApiVersion(major, minor)) { }
        public RemovedInApiVersionAttribute(ApiVersion version) : base(version) { }
    }
}
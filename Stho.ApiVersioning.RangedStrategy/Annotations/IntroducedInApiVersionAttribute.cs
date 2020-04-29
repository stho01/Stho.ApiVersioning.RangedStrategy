using System;
using Microsoft.Web.Http;

namespace Stho.ApiVersioning.RangedStrategy.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IntroducedInApiVersionAttribute : VersionHolderAttribute
    {
        public IntroducedInApiVersionAttribute(int major) : this(major, 0) { }
        public IntroducedInApiVersionAttribute(int major, int minor) : this(new ApiVersion(major, minor)) { }
        public IntroducedInApiVersionAttribute(ApiVersion version) : base(version) {  }
    }
}
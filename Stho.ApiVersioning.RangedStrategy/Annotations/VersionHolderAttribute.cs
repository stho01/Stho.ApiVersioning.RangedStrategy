using System;
using Microsoft.Web.Http;

namespace Stho.ApiVersioning.RangedStrategy.Annotations
{
    public abstract class VersionHolderAttribute : Attribute
    {
        protected VersionHolderAttribute(ApiVersion version) { Version = version; }
        public ApiVersion Version { get; }
    }
}
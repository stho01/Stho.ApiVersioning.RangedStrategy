using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Http;

namespace Stho.ApiVersioning.RangedStrategy
{
    public class ApiVersions : IEnumerable<ApiVersion>
    {
        private readonly HashSet<ApiVersion> _versions = new HashSet<ApiVersion>();

        public ApiVersion[] GetEarlierVersions(ApiVersion to)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            return this.Where(x => x < to).ToArray();
        }

        public ApiVersion[] GetLaterVersions(ApiVersion from)
        {
            if (from == null)
                throw new ArgumentNullException(nameof(from));

            return this.Where(x => x > from).ToArray();
        }

        public ApiVersion[] GetVersions(ApiVersion from, ApiVersion to)
        {
            if(from == null)
                throw new ArgumentNullException(nameof(from));

            var toOrLatest = to ?? this.Latest();

            return this.Where(x => x.IsAvailableIn(from, toOrLatest)).ToArray();
        }

        public ApiVersion Latest()
        {
            return this.OrderByDescending(x => x.MajorVersion)
                        .ThenBy(x => x.MinorVersion)
                        .FirstOrDefault();
        }

        public IEnumerator<ApiVersion> GetEnumerator() => _versions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _versions.GetEnumerator();

        public void Add(int major) => Add(major, 0);
        public void Add(int major, int minor) => Add(new ApiVersion(major, minor));
        public void Add(ApiVersion item)
        {
            _versions.Add(item);
        }
    }
}
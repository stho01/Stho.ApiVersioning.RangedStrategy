using System;
using System.Linq;
using Microsoft.Web.Http;
using Stho.ApiVersioning.RangedStrategy;
using Xunit;
using Assert = Xunit.Assert;

namespace CustomAttributeExample.UnitTests
{
    public class ApiVersionsTests
    {
        public readonly ApiVersion V1_0 = new ApiVersion(1, 0);
        public readonly ApiVersion V2_0 = new ApiVersion(2, 0);
        public readonly ApiVersion V2_1 = new ApiVersion(2, 1);
        public readonly ApiVersion V3_0 = new ApiVersion(3, 0);
        public readonly ApiVersion V3_1 = new ApiVersion(3, 1);
        public readonly ApiVersion V3_2 = new ApiVersion(3, 2);
        public readonly ApiVersion V4_0 = new ApiVersion(4, 0);
        public readonly ApiVersions Versions;

        public ApiVersionsTests()
        {
            Versions = new ApiVersions { V1_0, V2_0, V2_1, V3_0, V3_1, V3_2, V4_0 };
        }

        [Fact]
        public void GetLaterVersions_Returns_LaterVersions()
        {
            var laterVersions = Versions.GetLaterVersions(V3_0);
            var expected = new[] {V3_1, V3_2, V4_0};
            Assert.Equal(expected, laterVersions);
        }

        [Fact]
        public void GetEarlierVersions_Returns_EarlierVersions()
        {
            var laterVersions = Versions.GetEarlierVersions(V2_1);
            var expected = new[] { V1_0, V2_0 };
            Assert.Equal(expected, laterVersions);
        }

        [Fact]
        public void GetVersions_Returns_VersionsInSelectedInterval()
        {
            var laterVersions = Versions.GetVersions(V2_0, V3_1);
            var expected = new[] {V2_0, V2_1, V3_0, V3_1};
            Assert.Equal(expected, laterVersions);
        }

        [Fact]
        public void Latest_Returns_LatestVersion()
        {
            var latestVersion = Versions.Latest();

            Assert.Equal(V4_0, latestVersion);
        }

        [Fact]
        public void Add_Should_AddVersionToCollection()
        {
            var v5_0 = new ApiVersion(5,0);
            Versions.Add(v5_0);
            Assert.Contains(Versions, version => version == v5_0);
        }

        [Fact]
        public void Add_Should_IgnoreDuplicates()
        {
            Versions.Add(V4_0);
            Versions.Add(V4_0);

            Assert.Equal(1, Versions.Count(x => x == V4_0));
        }
    }
}

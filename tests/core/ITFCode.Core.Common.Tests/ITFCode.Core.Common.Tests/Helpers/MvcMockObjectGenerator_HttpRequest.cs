using Microsoft.AspNetCore.Http;
using Moq;

namespace ITFCode.Core.Common.Tests.Helpers
{
    public static partial class MvcMockObjectGenerator
    {
        public static IRequestCookieCollection GetCookies()
        {
            var mockCookies = new Mock<IRequestCookieCollection>();

            mockCookies.Setup(c => c["Key1"]).Returns("Value1");
            mockCookies.Setup(c => c.ContainsKey("Key1")).Returns(true);

            return mockCookies.Object;
        }
    }
}
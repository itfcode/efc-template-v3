using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;

namespace ITFCode.Core.Common.Tests.Helpers
{
    public static partial class MvcMockObjectGenerator
    {
        public static ConnectionInfo GetConnectionInfo()
        {
            var mockConnectionInfo = new Mock<ConnectionInfo>();

            mockConnectionInfo.SetupGet(x => x.LocalIpAddress).Returns(new IPAddress(50));
            mockConnectionInfo.SetupGet(x => x.RemoteIpAddress).Returns(new IPAddress(150));

            return mockConnectionInfo.Object;
        }

        public static HttpRequest GenerateHttpRequest(IQueryCollection? query = default,
            bool? hasFormContentType = default,
            Stream? body = default,
            string? contentType = default,
            long? contentLength = default,
            IRequestCookieCollection? cookies = default,
            IHeaderDictionary? headers = default,
            string? protocol = default,
            QueryString? queryString = default,
            PathString? path = default,
            HostString? host = default,
            bool? isHttps = default,
            string? scheme = default,
            string? method = default,
            HttpContext? httpContext = default,
            IFormCollection? form = default)
        {
            var mockRequest = new Mock<HttpRequest>();

            mockRequest.SetupGet(r => r.Query).Returns(query ?? new Mock<IQueryCollection>().Object);
            mockRequest.SetupGet(r => r.HasFormContentType).Returns(hasFormContentType ?? default);
            mockRequest.SetupGet(r => r.Body).Returns(body ?? new Mock<Stream>().Object);
            mockRequest.SetupGet(r => r.ContentType).Returns(contentType ?? "ContentType");
            mockRequest.SetupGet(r => r.ContentLength).Returns(contentLength ?? default);
            mockRequest.SetupGet(r => r.Cookies).Returns(cookies ?? GetCookies());
            mockRequest.SetupGet(r => r.Headers).Returns(headers ?? new Mock<IHeaderDictionary>().Object);
            mockRequest.SetupGet(r => r.Protocol).Returns(protocol ?? string.Empty);
            mockRequest.SetupGet(r => r.QueryString).Returns(queryString ?? new QueryString());
            mockRequest.SetupGet(r => r.Path).Returns(path ?? new PathString());
            mockRequest.SetupGet(r => r.Host).Returns(host ?? new HostString());
            mockRequest.SetupGet(r => r.IsHttps).Returns(isHttps ?? default);
            mockRequest.SetupGet(r => r.Scheme).Returns(scheme ?? string.Empty);
            mockRequest.SetupGet(r => r.Method).Returns(method ?? string.Empty);
            mockRequest.SetupGet(r => r.HttpContext).Returns(httpContext ?? new Mock<HttpContext>().Object);
            mockRequest.SetupGet(r => r.Form).Returns(form ?? new Mock<IFormCollection>().Object);

            return mockRequest.Object;
        }
    }
}
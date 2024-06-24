using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Reflection;
using System.Security.Claims;

namespace ITFCode.Core.Common.Tests.Helpers
{
    public static partial class MvcMockObjectGenerator
    {
        public static ControllerActionDescriptor GetControllerActionDescriptor(
            string? controllerName = default,
            string? actionName = default,
            MethodInfo? methodInfo = default,
            TypeInfo? controllerTypeInfo = default,
            string? displayName = default,
            string? id = default,
            IDictionary<string, string?>? routeValues = default,
            AttributeRouteInfo? attributeRouteInfo = default,
            IList<IActionConstraintMetadata>? actionConstraints = default,
            IList<object>? endpointMetadata = default,
            IList<ParameterDescriptor>? parameters = default,
            IList<ParameterDescriptor>? boundProperties = default,
            IList<FilterDescriptor>? filterDescriptors = default,
            IDictionary<object, object?>? properties = default)
        {
            var mockControllerActionDescriptor = new Mock<ControllerActionDescriptor>();

            mockControllerActionDescriptor.SetupGet(x => x.ControllerName)
                .Returns(controllerName ?? "ControllerName123");

            mockControllerActionDescriptor.SetupGet(x => x.ActionName)
                .Returns(actionName ?? "ActionName123");

            mockControllerActionDescriptor.SetupGet(x => x.MethodInfo)
                .Returns(methodInfo ?? default);

            mockControllerActionDescriptor.SetupGet(x => x.ControllerTypeInfo)
                .Returns(controllerTypeInfo ?? default);

            mockControllerActionDescriptor.SetupGet(x => x.DisplayName)
                .Returns(displayName ?? default);

            mockControllerActionDescriptor.SetupGet(x => x.Id)
                .Returns(id ?? Guid.NewGuid().ToString());

            mockControllerActionDescriptor.SetupGet(x => x.RouteValues)
                .Returns(routeValues ?? new Dictionary<string, string?>());

            mockControllerActionDescriptor.SetupGet(x => x.AttributeRouteInfo)
                .Returns(attributeRouteInfo ?? default);

            mockControllerActionDescriptor.SetupGet(x => x.ActionConstraints)
                .Returns(actionConstraints ?? default);

            mockControllerActionDescriptor.SetupGet(x => x.EndpointMetadata)
                .Returns(endpointMetadata ?? new List<object>());

            mockControllerActionDescriptor.SetupGet(x => x.Parameters)
                .Returns(parameters ?? new List<ParameterDescriptor>());

            mockControllerActionDescriptor.SetupGet(x => x.BoundProperties)
                .Returns(boundProperties ?? new List<ParameterDescriptor>());

            mockControllerActionDescriptor.SetupGet(x => x.FilterDescriptors)
                .Returns(filterDescriptors ?? new List<FilterDescriptor>());

            mockControllerActionDescriptor.SetupGet(x => x.Properties)
                .Returns(properties ?? new Dictionary<object, object?>());

            return mockControllerActionDescriptor.Object;
        }

        public static RouteData GetRouteData(
            RouteValueDictionary? dataTokens = default,
            IList<IRouter>? routers = default,
            RouteValueDictionary? values = default)
        {
            var mockRouteData = new Mock<RouteData>();

            mockRouteData.SetupGet(x => x.DataTokens).Returns(dataTokens ?? new Mock<RouteValueDictionary>().Object);
            mockRouteData.SetupGet(x => x.Routers).Returns(routers ?? new List<IRouter>());
            mockRouteData.SetupGet(x => x.Values).Returns(values ?? new RouteValueDictionary());

            return mockRouteData.Object;
        }

        public static HttpContext GenerateHttpContext(
            IFeatureCollection? features = default,
            HttpRequest? request = default,
            HttpResponse? response = default,
            ConnectionInfo? connection = default,
            AuthenticationManager? authenticationManager = default,
            WebSocketManager? webSockets = default,
            ClaimsPrincipal? user = default,
            IDictionary<object, object?>? items = default,
            IServiceProvider? requestServices = default,
            CancellationToken? requestAborted = default,
            string? traceIdentifier = default,
            ISession? session = default) // AuthenticationManager
        {
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.SetupGet(c => c.Features).Returns(features ?? new Mock<IFeatureCollection>().Object);
            mockHttpContext.SetupGet(c => c.Request).Returns(request ?? GenerateHttpRequest());
            mockHttpContext.SetupGet(c => c.Response).Returns(response ?? new Mock<HttpResponse>().Object);
            mockHttpContext.SetupGet(c => c.Connection).Returns(connection ?? GetConnectionInfo());
            mockHttpContext.SetupGet(c => c.Authentication).Returns(authenticationManager ?? new Mock<AuthenticationManager>().Object);
            mockHttpContext.SetupGet(c => c.WebSockets).Returns(webSockets ?? new Mock<WebSocketManager>().Object);
            mockHttpContext.SetupGet(c => c.User).Returns(user ?? new Mock<ClaimsPrincipal>().Object);
            mockHttpContext.SetupGet(c => c.Items).Returns(items ?? new Mock<IDictionary<object, object?>>().Object);
            mockHttpContext.SetupGet(c => c.RequestServices).Returns(requestServices ?? new Mock<IServiceProvider>().Object);
            mockHttpContext.SetupGet(c => c.RequestAborted).Returns(requestAborted ?? new CancellationTokenSource().Token);
            mockHttpContext.SetupGet(c => c.TraceIdentifier).Returns(traceIdentifier ?? string.Empty);
            mockHttpContext.SetupGet(c => c.Session).Returns(session ?? new Mock<ISession>().Object);

            return mockHttpContext.Object;
        }


        public static ModelStateDictionary GetModelStateDictionary()
        {
            var modelStateDictionary = new Mock<ModelStateDictionary>();
            return modelStateDictionary.Object;
        }
    }
}
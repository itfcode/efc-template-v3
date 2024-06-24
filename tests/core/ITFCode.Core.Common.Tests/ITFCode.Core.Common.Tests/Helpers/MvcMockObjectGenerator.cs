using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace ITFCode.Core.Common.Tests.Helpers
{
    public static partial class MvcMockObjectGenerator
    {
        public static ControllerContext GetControllerContext(HttpContext? httpContext = default,
            RouteData? routeData = default,
            ControllerActionDescriptor? actionDescriptor = default,
            ModelStateDictionary? modelState = default)
        {
            var context = new ActionContext(
                httpContext: httpContext ?? GenerateHttpContext(),
                routeData: routeData ?? GetRouteData(),
                actionDescriptor: actionDescriptor ?? GetControllerActionDescriptor(),
                modelState: modelState ?? GetModelStateDictionary());

            return new ControllerContext(context);
        }
    }
}

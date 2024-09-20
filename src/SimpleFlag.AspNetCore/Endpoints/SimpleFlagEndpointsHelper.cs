using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Reflection;

namespace SimpleFlag.AspNetCore.Endpoints;
internal static class SimpleFlagEndpointsHelper
{
    /// <summary>
    /// Creates an endpoint.
    /// </summary>
    /// <param name="methods"></param>
    /// <param name="pattern"></param>
    /// <param name="requestDelegate"></param>
    /// <returns></returns>
    internal static Endpoint CreateEndpoint(string methods, string pattern, RequestDelegate requestDelegate, MethodInfo? methodInfo, IAcceptsMetadata? simpleFlagAcceptsMetadata = null)
    {
        var endpointBuilder = new RouteEndpointBuilder(
            requestDelegate: requestDelegate,
            routePattern: RoutePatternFactory.Parse(pattern),
            order: 0);

        endpointBuilder.Metadata.Add(new HttpMethodMetadata(new[] { methods }));
        if (methodInfo != null)
        {
            endpointBuilder.Metadata.Add(methodInfo);
        }

        if (simpleFlagAcceptsMetadata is not null)
        {
            endpointBuilder.Metadata.Add(simpleFlagAcceptsMetadata);
        }

        return endpointBuilder.Build();
    }
}

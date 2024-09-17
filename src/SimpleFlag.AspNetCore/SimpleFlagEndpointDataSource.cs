using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace SimpleFlag.AspNetCore;

/// <summary>
/// 
/// </summary>
public class SimpleFlagEndpointDataSource : EndpointDataSource
{
    private List<Endpoint>? _endpoints;

    private readonly SimpleFlagEndpointOptions _simpleFlagEndpointOptions;
    private readonly ISimpleFlagClient _simpleFlagClient;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="simpleFlagClient"></param>
    public SimpleFlagEndpointDataSource(IOptions<SimpleFlagEndpointOptions> options, ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagEndpointOptions = options.Value;
        _simpleFlagClient = simpleFlagClient;

        _endpoints = BuildEndpoints();
    }

    /// <summary>
    /// 
    /// </summary>
    public override IReadOnlyList<Endpoint> Endpoints => _endpoints;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<Endpoint> BuildEndpoints()
    {
        var endpointPrefix = _simpleFlagEndpointOptions.EndpointPrefix ?? "simpleflag";
        endpointPrefix = endpointPrefix.TrimStart('/').TrimEnd('/');

        var featureFlagEndpoint = new SimpleFlagEndpoints(_simpleFlagClient);

        var addFeatureFlagEndpoint = CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/flag",
            featureFlagEndpoint.AddFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddFeatureFlagAsync)),
            new AcceptsMetadata(["application/json"], typeof(CreateFeatureFlagRequest), false)
        );

        var editFeatureFlagEndpoint = CreateEndpoint
        (
            HttpMethods.Put,
            $"{endpointPrefix}/flag",
            featureFlagEndpoint.EditFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.EditFeatureFlagAsync)),
            new AcceptsMetadata(["application/json"], typeof(CreateFeatureFlagRequest), false)
        );

        return new List<Endpoint> { addFeatureFlagEndpoint, editFeatureFlagEndpoint };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override IChangeToken GetChangeToken() => NullChangeToken.Singleton;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methods"></param>
    /// <param name="pattern"></param>
    /// <param name="requestDelegate"></param>
    /// <returns></returns>
    private Endpoint CreateEndpoint(string methods, string pattern, RequestDelegate requestDelegate, MethodInfo? methodInfo, IAcceptsMetadata? simpleFlagAcceptsMetadata)
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

        // Optionally, add RouteNameMetadata
        //endpointBuilder.Metadata.Add(new RouteNameMetadata(operationId));

        // Add EndpointGroupNameMetadata
        //endpointBuilder.Metadata.Add(new SimpleFlagGroupName());

        if (simpleFlagAcceptsMetadata is not null)
        {
            endpointBuilder.Metadata.Add(simpleFlagAcceptsMetadata);
        }


        return endpointBuilder.Build();
    }
}

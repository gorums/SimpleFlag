using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SimpleFlag.Core;

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
    }

    /// <summary>
    /// 
    /// </summary>
    public override IReadOnlyList<Endpoint> Endpoints
    {
        get
        {
            if (_endpoints is null)
            {
                _endpoints = BuildEndpoints();
            }

            return _endpoints;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<Endpoint> BuildEndpoints()
    {
        var endpointPrefix = _simpleFlagEndpointOptions.EndpointPrefix ?? "simpleflag";
        endpointPrefix = endpointPrefix.TrimStart('/').TrimEnd('/');

        var featureFlagEndpoint = new SimpleFlagEndpoints(_simpleFlagClient);

        var addFeatureFlagEndpoint = CreateEndpoint(HttpMethods.Post, $"{endpointPrefix}/add-flag", featureFlagEndpoint.AddFeatureFlagDelegateAsync);

        return new List<Endpoint> { addFeatureFlagEndpoint };
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
    private Endpoint CreateEndpoint(string methods, string pattern, RequestDelegate requestDelegate)
    {
        var endpointBuilder = new RouteEndpointBuilder(
            requestDelegate: requestDelegate,
            routePattern: RoutePatternFactory.Parse(pattern),
            order: 0);

        endpointBuilder.Metadata.Add(new HttpMethodMetadata([methods]));

        return endpointBuilder.Build();
    }
}

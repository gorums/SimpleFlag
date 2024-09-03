using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SimpleFlag.AspNetCore.Features;
using SimpleFlag.Core;

namespace SimpleFlag.AspNetCore;
public class SimpleFlagEndpointDataSource : EndpointDataSource
{
    private List<Endpoint>? _endpoints;

    private readonly SimpleFlagEndpointOptions _simpleFlagEndpointOptions;
    private readonly ISimpleFlagClient _simpleFlagClient;

    public SimpleFlagEndpointDataSource(IOptions<SimpleFlagEndpointOptions> options, ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagEndpointOptions = options.Value;
        _simpleFlagClient = simpleFlagClient;
    }

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

    private List<Endpoint> BuildEndpoints()
    {
        var endpointPrefix = _simpleFlagEndpointOptions.EndpointPrefix ?? "simple-flag/";
        if (!endpointPrefix.EndsWith("/"))
        {
            endpointPrefix += "/";
        }

        var featureFlagEndpoint = new FeatureFlagEndpoint(_simpleFlagClient);
        var addFeatureFlagEndpoint = CreateEndpoint(HttpMethods.Post, $"{endpointPrefix}add-flag", featureFlagEndpoint.AddFeatureFlagDelegate);

        return new List<Endpoint> { addFeatureFlagEndpoint };
    }
    public override IChangeToken GetChangeToken() => NullChangeToken.Singleton;

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

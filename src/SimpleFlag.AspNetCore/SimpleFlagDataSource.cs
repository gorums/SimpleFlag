using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

namespace SimpleFlag.AspNetCore;
public class SimpleFlagDataSource : EndpointDataSource
{
    private List<Endpoint>? _endpoints;

    private readonly List<Action<EndpointBuilder>> _conventions;

    private readonly SimpleFlagEndpointOptions _simpleFlagEndpointOptions;
    public SimpleFlagDataSource(IOptions<SimpleFlagEndpointOptions> options)
    {
        _simpleFlagEndpointOptions = options.Value;
        _conventions = new List<Action<EndpointBuilder>>();
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
        var endpoint = CreateEndpoint("add-feature-flag", context => context.Response.WriteAsync("hello"));

        return new List<Endpoint> { endpoint };
    }

    public override IChangeToken GetChangeToken() => NullChangeToken.Singleton;

    private Endpoint CreateEndpoint(string pattern, RequestDelegate requestDelegate)
    {
        var endpointBuilder = new RouteEndpointBuilder(
            requestDelegate: requestDelegate,
            routePattern: RoutePatternFactory.Parse(pattern),
            order: 0);

        // this is not working
        if (_simpleFlagEndpointOptions.ShowInOpenAPI)
        {
            endpointBuilder.Metadata.Add(new OpenApiOperation
            {
                OperationId = "AddFeatureFlag",
                Summary = "Adds a new feature flag",
                Description = "Endpoint to add a new feature flag",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "FeatureFlags" } }
            });
        }

        return endpointBuilder.Build();
    }
}

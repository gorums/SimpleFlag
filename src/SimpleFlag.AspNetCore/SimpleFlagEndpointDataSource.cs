using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SimpleFlag.AspNetCore.Endpoints;
using SimpleFlag.AspNetCore.Endpoints.Dtos;

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
    public override IChangeToken GetChangeToken() => NullChangeToken.Singleton;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<Endpoint> BuildEndpoints()
    {
        var endpointPrefix = _simpleFlagEndpointOptions.EndpointPrefix ?? "simpleflag";
        endpointPrefix = endpointPrefix.TrimStart('/').TrimEnd('/');

        var simpleFlagEndpointsHandle = new SimpleFlagEndpointsHandler(_simpleFlagClient);

        return new List<Endpoint>
        {
            // Feature Flag Endpoints
            CreateGetFeatureFlagsEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateAddFeatureFlagEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateUpdateFeatureFlagEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateDeleteFeatureFlagEndpoint(endpointPrefix, simpleFlagEndpointsHandle),

            // Segment Endpoints
            CreateGetSegmentsEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateAddSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateUpdateSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateDeleteSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),            
            
            // User Endpoints
            CreateGetUsersEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateAddUsersEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateUpdateUsersEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateDeleteUsersEndpoint(endpointPrefix, simpleFlagEndpointsHandle),

            // Feature Flag Segment Endpoints
            CreateAddSegmentToFeatureFlagEndpoint(endpointPrefix, simpleFlagEndpointsHandle),

            // User Segment Endpoints            
            CreateAddUsersToSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateDeleteUsersFromSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
            CreateCleanUsersOnSegmentEndpoint(endpointPrefix, simpleFlagEndpointsHandle),

            // Domain Endpoints
            CreateGetDomainsEndpoint(endpointPrefix, simpleFlagEndpointsHandle),
        };
    }

    #region Feature Flag Endpoints

    /// <summary>
    /// Creates the endpoint for get a feature flag by domain.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateGetFeatureFlagsEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Get,
            $"{endpointPrefix}/flags/" + "{domain}",
            simpleFlagEndpointsHandle.GetFeatureFlagsDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.GetFeatureFlagsAsync))
        );

    /// <summary>
    /// Creates the endpoint for adding a feature flag.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateAddFeatureFlagEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/flags",
            simpleFlagEndpointsHandle.AddFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddFeatureFlagAsync)),
            new AcceptsMetadata(["application/json"], typeof(CreateFeatureFlagRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for update a feature flag.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateUpdateFeatureFlagEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Put,
            $"{endpointPrefix}/flags/" + "{id:guid}",
            simpleFlagEndpointsHandle.AddFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.UpdateFeatureFlagAsync)),
            new AcceptsMetadata(["application/json"], typeof(UpdateFeatureFlagRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for delete a feature flag.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateDeleteFeatureFlagEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Delete,
            $"{endpointPrefix}/flags/" + "{id:guid}",
            simpleFlagEndpointsHandle.DeleteFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.DeleteFeatureFlagAsync))
        );

    #endregion Feature Flag Endpoints

    #region Segment Endpoints
    /// <summary>
    /// Creates the endpoint for getting segments.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateGetSegmentsEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Get,
            $"{endpointPrefix}/segments",
            simpleFlagEndpointsHandle.GetSegmentsDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.GetSegmentsAsync))
        );


    /// <summary>
    /// Creates the endpoint for adding a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateAddSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/segments",
            simpleFlagEndpointsHandle.AddSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddSegmentAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(CreateSegmentRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for updating a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateUpdateSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Put,
            $"{endpointPrefix}/segments/" + "{id:guid}",
            simpleFlagEndpointsHandle.UpdateSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.UpdateSegmentAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(UpdateSegmentRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for deleting a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateDeleteSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Delete,
            $"{endpointPrefix}/segments/" + "{id:guid}",
            simpleFlagEndpointsHandle.DeleteSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.DeleteSegmentAsync))
        );

    #endregion Segment Endpoints

    #region User Endpoints

    /// <summary>
    /// Creates the endpoint for getting users from a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateGetUsersEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Get,
            $"{endpointPrefix}/users/" + "{segment}",
            simpleFlagEndpointsHandle.GetUsersDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.GetUsersAsync))
        );

    /// <summary>
    /// Creates the endpoint for adding users.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateAddUsersEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/users",
            simpleFlagEndpointsHandle.AddUsersDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddUsersAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(AddUsersRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for updating users.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateUpdateUsersEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Put,
            $"{endpointPrefix}/users",
            simpleFlagEndpointsHandle.UpdateUsersDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.UpdateUsersAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(UpdateUsersRequest), false)
        );


    /// <summary>
    /// Creates the endpoint for removing users.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateDeleteUsersEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Delete,
            $"{endpointPrefix}/users",
            simpleFlagEndpointsHandle.RemoveUsersDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.DeleteUsersAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(RemoveUsersRequest), false)
        );

    #endregion User Endpoints

    #region Feature Flag Segment Endpoints
    /// <summary>
    /// Creates the endpoint for adding a segment to a feature flag.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateAddSegmentToFeatureFlagEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/flag/" + "{id:guid}/segment/{segment}",
            simpleFlagEndpointsHandle.AddSegmentToFeatureFlagDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddSegmentToFeatureFlagAsync))
        );

    #endregion Feature Flag Segment Endpoints

    #region User Segment Endpoints
    /// <summary>
    /// Creates the endpoint for adding users to a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateAddUsersToSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Post,
            $"{endpointPrefix}/segment/" + "{segment}/users",
            simpleFlagEndpointsHandle.AddUsersToSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.AddUsersToSegmentAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(AddUsersToSegmentRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for removing users from a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateDeleteUsersFromSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Delete,
            $"{endpointPrefix}/segment/" + "{segment}/users",
            simpleFlagEndpointsHandle.RemoveUsersFromSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.DeleteUsersFromSegmentAsync)),
            new AcceptsMetadata(new[] { "application/json" }, typeof(RemoveUsersFromSegmentRequest), false)
        );

    /// <summary>
    /// Creates the endpoint for cleaning users on a segment.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateCleanUsersOnSegmentEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Delete,
            $"{endpointPrefix}/segment/" + "{segment}/users/clean",
            simpleFlagEndpointsHandle.CleanUsersOnSegmentDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.CleanUsersOnSegmentAsync))
        );

    #endregion User Segment Endpoints

    #region Domain Endpoints
    /// <summary>
    /// Creates the endpoint for getting domains.
    /// </summary>
    /// <param name="endpointPrefix">The endpoint prefix</param>
    /// <param name="simpleFlagEndpointsHandle">The <see cref="SimpleFlagEndpointsHandler"/></param>
    /// <returns>An <see cref="Endpoint"/></returns>
    private Endpoint CreateGetDomainsEndpoint(string endpointPrefix, SimpleFlagEndpointsHandler simpleFlagEndpointsHandle) =>
        SimpleFlagEndpointsHelper.CreateEndpoint
        (
            HttpMethods.Get,
            $"{endpointPrefix}/domains",
            simpleFlagEndpointsHandle.GetDomainsDelegateAsync,
            typeof(SimpleFlagEndpoints).GetMethod(nameof(SimpleFlagEndpoints.GetDomainsAsync))
        );

    #endregion Domain Endpoints
}

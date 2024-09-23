using Microsoft.AspNetCore.Mvc;
using SimpleFlag.AspNetCore.Endpoints.Dtos;
using SimpleFlag.Core.Entities;

namespace SimpleFlag.AspNetCore.Endpoints;

/// <summary>
/// Represents the endpoints for handling feature flags in the SimpleFlag.AspNetCore namespace.
/// </summary>
internal class SimpleFlagEndpoints
{
    private readonly ISimpleFlagClient _simpleFlagClient;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagEndpoints class.
    /// </summary>
    /// <param name="simpleFlagClient">The SimpleFlag client used for handling feature flags.</param>
    public SimpleFlagEndpoints(ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagClient = simpleFlagClient;
    }

    #region Feature Flags
    /// <summary>
    /// Handles the logic for getting a feature flag.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FeatureFlagDto>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken)
    {
        var featureFlags = await _simpleFlagClient.GetFeatureFlagsAsync(domain, cancellationToken);
        return featureFlags.Select(ff => new FeatureFlagDto(ff.Name, ff.Description, ff.Key, ff.Enabled, ff.Archived, domain));
    }

    /// <summary>
    /// Handles the logic for getting a feature flag.
    /// </summary>
    /// <param name="featureFlagDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CreateFeatureFlagResponse?> AddFeatureFlagAsync([FromBody] CreateFeatureFlagRequest featureFlagDto, CancellationToken cancellationToken)
    {
        // Add the feature flag using the SimpleFlagClient
        var featureFlag = new FeatureFlag
        {
            Name = featureFlagDto.Name,
            Description = featureFlagDto.Description,
            Key = featureFlagDto.Key,
            Enabled = featureFlagDto.Enabled
        };

        var result = await _simpleFlagClient.AddFeatureFlagAsync(featureFlagDto.Domain, featureFlag, cancellationToken);
        return new CreateFeatureFlagResponse(result.Name, result.Description, result.Key, result.Enabled, featureFlagDto.Domain);
    }

    /// <summary>
    /// Handles the logic for updating a feature flag.
    /// </summary>
    /// <param name="flagId"></param>
    /// <param name="featureFlagDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UpdateFeatureFlagResponse> UpdateFeatureFlagAsync(Guid flagId, [FromBody] UpdateFeatureFlagRequest featureFlagDto, CancellationToken cancellationToken)
    {
        var featureFlag = new FeatureFlag
        {
            Id = flagId,
            Name = featureFlagDto.Name,
            Description = featureFlagDto.Description,
            Enabled = featureFlagDto.Enabled,
            Archived = featureFlagDto.Archive
        };

        var result = await _simpleFlagClient.UpdateFeatureFlagAsync(featureFlagDto.Domain, featureFlag, cancellationToken);
        return new UpdateFeatureFlagResponse(result.Id, result.Name, result.Description, result.Key, result.Enabled, result.Archived, featureFlagDto.Domain);
    }

    /// <summary>
    /// Handles the logic for deleting a feature flag.
    /// </summary>
    /// <param name="flagId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken)
    {
        await _simpleFlagClient.DeleteFeatureFlagAsync(flagId, cancellationToken);
    }

    #endregion Feature Flags

    #region Segments

    /// <summary>
    /// Handles the logic for getting segments.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<SegmentDto>> GetSegmentsAsync(CancellationToken cancellationToken)
    {
        var segments = await _simpleFlagClient.GetSegmentsAsync(cancellationToken);
        return segments.Select(s => new SegmentDto(s.Name, s.Description));
    }

    /// <summary>
    /// Handles the logic for adding a segment.
    /// </summary>
    /// <param name="createSegmentRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CreateSegmentResponse> AddSegmentAsync(CreateSegmentRequest createSegmentRequest, CancellationToken cancellationToken)
    {
        var segment = new FeatureFlagSegment(createSegmentRequest.Name, createSegmentRequest.Description);

        var result = await _simpleFlagClient.AddSegmentAsync(segment, cancellationToken);

        return new CreateSegmentResponse(result.Id, result.Name, result.Description);
    }

    /// <summary>
    /// Handles the logic for updating a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="segmentDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UpdateSegmentResponse> UpdateSegmentAsync(Guid segmentId, UpdateSegmentRequest segmentDto, CancellationToken cancellationToken)
    {
        var segment = new FeatureFlagSegment(segmentId, segmentDto.Name, segmentDto.Description);

        var result = await _simpleFlagClient.UpdateSegmentAsync(segment, cancellationToken);
        return new UpdateSegmentResponse(result.Id, result.Name, result.Description);
    }

    /// <summary>
    /// Handles the logic for deleting a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken)
    {
        await _simpleFlagClient.DeleteSegmentAsync(segmentId, cancellationToken);
    }

    #endregion Segments

    #region Users

    /// <summary>
    /// Handles the logic for getting users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserDto>> GetUsersAsync(string? segment, CancellationToken cancellationToken)
    {
        var users = await _simpleFlagClient.GetUsersAsync(segment, cancellationToken);
        return users.Select(u => new UserDto(u.Name, u.Attributes));
    }

    /// <summary>
    /// Handles the logic for getting users.
    /// </summary>
    /// <param name="addUsersRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AddUserDto>> AddUsersAsync(AddUsersRequest addUsersRequest, CancellationToken cancellationToken)
    {
        var users = addUsersRequest.Users.Select(u => new SimpleFlagUser(u.Name)).ToList();
        var result = await _simpleFlagClient.AddUsersAsync(users, cancellationToken);

        return result.Select(u => new AddUserDto(u.Id, u.Name));
    }

    /// <summary>
    /// Handles the logic for updating users.
    /// </summary>
    /// <param name="usersDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UpdateUserDto>> UpdateUsersAsync(UpdateUsersRequest usersDto, CancellationToken cancellationToken)
    {
        var users = usersDto.Users.Select(u => new SimpleFlagUser(u.Name)).ToList();
        var result = await _simpleFlagClient.UpdateUsersAsync(users, cancellationToken);

        return result.Select(u => new UpdateUserDto(u.Id, u.Name));
    }

    /// <summary>
    /// Handles the logic for deleting users.
    /// </summary>
    /// <param name="removeUsersRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteUsersAsync(RemoveUsersRequest removeUsersRequest, CancellationToken cancellationToken)
    {
        var userIds = removeUsersRequest.Users.Select(u => u.Id).ToList();
        await _simpleFlagClient.DeleteUsersAsync(userIds, cancellationToken);
    }

    #endregion Users

    #region Feature Flag Segments

    /// <summary>
    /// Handles the logic for getting segments from a feature flag.
    /// </summary>
    /// <param name="flagId"></param>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken)
    {
        await _simpleFlagClient.AddSegmentToFeatureFlagAsync(segment, flagId, cancellationToken);
    }

    #endregion Feature Flag Segments

    #region User Segments    

    /// <summary>
    /// Handles the logic for adding users to a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="addUsersToSegmentRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddUsersToSegmentAsync(AddUsersToSegmentRequest addUsersToSegmentRequest, string segment, CancellationToken cancellationToken)
    {
        var users = addUsersToSegmentRequest.Users.Select(u => new SimpleFlagUser(u.Name)).ToList();

        await _simpleFlagClient.AddUsersToSegmentAsync(users, segment, cancellationToken);
    }

    /// <summary>
    /// Handles the logic for deleting users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="removeUsersFromSegmentRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteUsersFromSegmentAsync(string segment, RemoveUsersFromSegmentRequest removeUsersFromSegmentRequest, CancellationToken cancellationToken)
    {
        var userIds = removeUsersFromSegmentRequest.Users.Select(u => u.Id).ToList();

        await _simpleFlagClient.DeleteUsersFromSegmentAsync(segment, userIds, cancellationToken);
    }

    /// <summary>
    /// Handles the logic for cleaning users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken)
    {
        await _simpleFlagClient.CleanUsersOnSegmentAsync(segment, cancellationToken);
    }

    #endregion User Segments

    #region Domains

    /// <summary>
    /// Handles the logic for getting domains.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<DomainDto>> GetDomainsAsync(CancellationToken cancellationToken)
    {
        var domains = await _simpleFlagClient.GetDomainsAsync(cancellationToken);

        return domains.Select(d => new DomainDto(d.Id, d.Name, d.Description));
    }

    #endregion Domains
}

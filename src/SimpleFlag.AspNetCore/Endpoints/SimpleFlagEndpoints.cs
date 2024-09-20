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
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> GetFeatureFlagsAsync(string? domain, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
    public Task<UpdateFeatureFlagResponse> UpdateFeatureFlagAsync(Guid flagId, [FromBody] UpdateFeatureFlagRequest featureFlagDto, CancellationToken cancellationToken)
    {
        return Task.FromResult(new UpdateFeatureFlagResponse(featureFlagDto.Name, featureFlagDto.Description, flagId.ToString(), featureFlagDto.Enabled, featureFlagDto.Archive, featureFlagDto.Domain));
    }

    /// <summary>
    /// Handles the logic for deleting a feature flag.
    /// </summary>
    /// <param name="flagId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    #endregion Feature Flags

    #region Segments

    /// <summary>
    /// Handles the logic for getting segments.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> GetSegmentsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for adding a segment.
    /// </summary>
    /// <param name="createSegmentRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<CreateSegmentResponse> AddSegmentAsync(CreateSegmentRequest createSegmentRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for updating a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="segmentDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> UpdateSegmentAsync(Guid segmentId, UpdateSegmentRequest segmentDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for deleting a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion Segments

    #region Users

    /// <summary>
    /// Handles the logic for getting users from a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> GetUsersAsync(string? segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for getting users.
    /// </summary>
    /// <param name="addUsersRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AddUsersResponse> AddUsersAsync(AddUsersRequest addUsersRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for updating users.
    /// </summary>
    /// <param name="usersDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> UpdateUsersAsync(UpdateUsersRequest usersDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for deleting users.
    /// </summary>
    /// <param name="removeUsersRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteUsersAsync(RemoveUsersRequest removeUsersRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion Users

    #region Feature Flag Segments

    /// <summary>
    /// Handles the logic for getting segments from a feature flag.
    /// </summary>
    /// <param name="flagId"></param>
    /// <param name="addSegmentToFeatureFlagRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddSegmentToFeatureFlagAsync(Guid flagId, string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AddUsersToSegmentResponse> AddUsersToSegmentAsync(string segment, AddUsersToSegmentRequest addUsersToSegmentRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for deleting users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="removeUsersFromSegmentRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteUsersFromSegmentAsync(string segment, RemoveUsersFromSegmentRequest removeUsersFromSegmentRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the logic for cleaning users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion User Segments

    #region Domains

    /// <summary>
    /// Handles the logic for getting domains.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> GetDomainsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion Domains
}

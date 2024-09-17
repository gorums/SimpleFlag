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

    /// <summary>
    /// Handles the logic for adding a feature flag.
    /// </summary>
    /// <param name="context">The HTTP context</param>
    /// <param name="simpleFlagClient">The SimpleFlag client</param>
    /// <returns>A task representing the asynchronous operation</returns>
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
    /// Handles the logic for adding a feature flag.
    /// </summary>
    /// <param name="context">The HTTP context</param>
    /// <param name="simpleFlagClient">The SimpleFlag client</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task<CreateFeatureFlagResponse?> EditFeatureFlagAsync([FromBody] CreateFeatureFlagRequest featureFlagDto, CancellationToken cancellationToken)
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
}

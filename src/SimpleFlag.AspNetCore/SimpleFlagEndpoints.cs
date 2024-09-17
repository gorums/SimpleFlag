using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFlag.Core.Entities;

namespace SimpleFlag.AspNetCore;

internal record CreateFeatureFlagRequest(string Name, string Description, string Key, bool Enabled, string Domain);

internal record CreateFeatureFlagResponse(string Name, string Description, string Key, bool Enabled, string Domain);

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
    /// Handles the AddFeatureFlagDelegate endpoint by calling the corresponding method in the AddFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the FeatureFlag from the request body
            var featureFlagDto = await context.Request.ReadFromJsonAsync<CreateFeatureFlagRequest>();

            if (featureFlagDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid feature flag data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await AddFeatureFlagAsync(featureFlagDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the feature flag.",
                Detail = ex.Message,
                Status = ex is SimpleFlagExistException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
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
    /// Handles the AddFeatureFlagDelegate endpoint by calling the corresponding method in the AddFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task EditFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the FeatureFlag from the request body
            var featureFlagDto = await context.Request.ReadFromJsonAsync<CreateFeatureFlagRequest>();

            if (featureFlagDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid feature flag data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await EditFeatureFlagAsync(featureFlagDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the feature flag.",
                Detail = ex.Message,
                Status = ex is SimpleFlagExistException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFlag.AspNetCore.Endpoints.Dtos;

namespace SimpleFlag.AspNetCore.Endpoints;

/// <summary>
/// Represents the endpoints for handling feature flags in the SimpleFlag.AspNetCore namespace.
/// </summary>
internal class SimpleFlagEndpointsHandler
{
    private readonly SimpleFlagEndpoints _simpleFlagEndpoints;
    /// <summary>
    /// Initializes a new instance of the SimpleFlagEndpoints class.
    /// </summary>
    /// <param name="simpleFlagClient">The SimpleFlag client used for handling feature flags.</param>
    public SimpleFlagEndpointsHandler(ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagEndpoints = new SimpleFlagEndpoints(simpleFlagClient);
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

            var result = await _simpleFlagEndpoints.AddFeatureFlagAsync(featureFlagDto, cancellationToken);

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

            var result = await _simpleFlagEndpoints.EditFeatureFlagAsync(featureFlagDto, cancellationToken);

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
}

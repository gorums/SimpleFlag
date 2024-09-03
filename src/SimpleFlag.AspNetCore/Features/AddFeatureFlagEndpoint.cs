using Microsoft.AspNetCore.Http;
using SimpleFlag.Core;
using SimpleFlag.Core.Models;

namespace SimpleFlag.AspNetCore.Features;
internal class AddFeatureFlagEndpoint
{
    /// <summary>
    /// Handles the logic for adding a feature flag.
    /// </summary>
    /// <param name="context">The HTTP context</param>
    /// <param name="simpleFlagClient">The SimpleFlag client</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public static async Task HandleAddFeatureFlagAsync(HttpContext context, ISimpleFlagClient simpleFlagClient)
    {
        // Deserialize the FeatureFlag from the request body
        var featureFlag = await context.Request.ReadFromJsonAsync<FeatureFlag>();

        if (featureFlag == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid feature flag data.");
            return;
        }

        // Get the cancellation token from the context
        var cancellationToken = context.RequestAborted;

        // Add the feature flag using the SimpleFlagClient
        var result = await simpleFlagClient.AddFeatureFlagAsync(featureFlag, cancellationToken);

        // Return the result
        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsJsonAsync(result);
    }
}

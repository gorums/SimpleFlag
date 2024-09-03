using Microsoft.AspNetCore.Http;
using SimpleFlag.Core;
using SimpleFlag.Core.Models;

namespace SimpleFlag.AspNetCore.Features;
internal class FeatureFlagEndpoint
{
    private readonly ISimpleFlagClient _simpleFlagClient;

    public FeatureFlagEndpoint(ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagClient = simpleFlagClient;
    }

    public async Task AddFeatureFlagDelegate(HttpContext context)
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
        var result = await _simpleFlagClient.AddFeatureFlagAsync(featureFlag, cancellationToken);

        // Return the result
        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsJsonAsync(result);
    }

}

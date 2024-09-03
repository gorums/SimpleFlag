using Microsoft.AspNetCore.Http;
using SimpleFlag.AspNetCore.Features;
using SimpleFlag.Core;

namespace SimpleFlag.AspNetCore;

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
    public async Task AddFeatureFlagDelegateAsync(HttpContext context) =>
        await AddFeatureFlagEndpoint.HandleAddFeatureFlagAsync(context, _simpleFlagClient);
}

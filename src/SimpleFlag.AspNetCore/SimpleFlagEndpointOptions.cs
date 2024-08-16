namespace SimpleFlag.AspNetCore;

/// <summary>
/// This class contains the extension methods for the IApplicationBuilder.
/// </summary>
public record SimpleFlagEndpointOptions
{
    public bool ShowInOpenAPI { get; set; }
}
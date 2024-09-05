namespace SimpleFlag.AspNetCore;

/// <summary>
/// This class contains the extension methods for the IApplicationBuilder.
/// </summary>
public record SimpleFlagEndpointOptions
{
    public string EndpointPrefix { get; set; } = "simpleflag";
}
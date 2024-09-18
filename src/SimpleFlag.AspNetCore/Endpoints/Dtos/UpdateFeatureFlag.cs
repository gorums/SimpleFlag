namespace SimpleFlag.AspNetCore.Endpoints.Dtos;
internal record UpdateFeatureFlagRequest(string Name, string Description, bool Enabled, bool Archive, string Domain);

internal record UpdateFeatureFlagResponse(string Name, string Description, string Key, bool Enabled, bool Archive, string Domain);

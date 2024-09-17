namespace SimpleFlag.AspNetCore.Endpoints.Dtos;
internal record CreateFeatureFlagRequest(string Name, string Description, string Key, bool Enabled, string Domain);

internal record CreateFeatureFlagResponse(string Name, string Description, string Key, bool Enabled, string Domain);

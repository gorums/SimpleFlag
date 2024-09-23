namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record FeatureFlagDto(string Name, string Description, string Key, bool Enabled, bool Archive, string Domain);

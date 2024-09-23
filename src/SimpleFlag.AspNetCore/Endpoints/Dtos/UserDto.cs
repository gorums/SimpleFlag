namespace SimpleFlag.AspNetCore.Endpoints.Dtos;
public record UserDto(string Name, Dictionary<string, string>? Attributes);

namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record UpdateSegmentRequest(string Name, string Description);

public record UpdateSegmentResponse(Guid id, string Name, string Description);
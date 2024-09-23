namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

internal record CreateSegmentRequest(string Name, string Description);

internal record CreateSegmentResponse(Guid Id, string Name, string Description);
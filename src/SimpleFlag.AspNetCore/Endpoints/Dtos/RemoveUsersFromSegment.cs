namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record RemoveUsersFromSegmentRequest(IList<RemoveUserDto> Users);
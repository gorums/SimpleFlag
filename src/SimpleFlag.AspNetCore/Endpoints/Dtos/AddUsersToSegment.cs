namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record AddUsersToSegmentRequest(IList<AddUserDto> Users);
namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record RemoveUserDto(Guid Id);
public record RemoveUsersRequest(IList<RemoveUserDto> Users);
namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record UpdateUserDto(Guid Id, string Name);
public record UpdateUsersRequest(IList<UpdateUserDto> Users);
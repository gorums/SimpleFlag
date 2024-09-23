namespace SimpleFlag.AspNetCore.Endpoints.Dtos;

public record AddUserDto(Guid? Id, string Name);

public record AddUsersRequest(IList<AddUserDto> Users);
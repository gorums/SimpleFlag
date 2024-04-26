namespace DemoApi.Postgresql.Features;

public record TodoDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedAt, DateTime UpdatedAt);

public record CreateTodoDto(string Title);

public record UpdateTodoDto(string Title, bool IsCompleted);

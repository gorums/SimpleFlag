namespace DemoApi.Postgresql.Features.Services;

public interface ITodoService
{
    Task<List<TodoDto>> GetTodoListAsync(CancellationToken cancellationToken = default);

    Task<TodoDto?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TodoDto> CreateTodoAsync(CreateTodoDto todo, CancellationToken cancellationToken = default);

    Task UpdateTodoAsync(Guid id, UpdateTodoDto todoToUpdate, CancellationToken cancellationToken = default);

    Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default);
}

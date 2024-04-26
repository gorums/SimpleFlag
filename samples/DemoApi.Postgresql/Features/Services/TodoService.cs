using DemoApi.Postgresql.Domain.Entities;
using DemoApi.PostgreSQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Postgresql.Features.Services;

public class TodoService : ITodoService
{
    private readonly DemoApiDbContext _demoApiDbContext;

    public TodoService(DemoApiDbContext demoApiDbContext)
    {
        _demoApiDbContext = demoApiDbContext;
    }

    public async Task<List<TodoDto>> GetTodoListAsync(CancellationToken cancellationToken = default)
    {
        List<Todo> todos = await _demoApiDbContext.Todos.ToListAsync(cancellationToken);

        return todos.Select(todo => new TodoDto(todo.Id, todo.Title, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt)).ToList();
    }

    public async Task<TodoDto?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Todo? todo = await _demoApiDbContext.Todos.FindAsync(id, cancellationToken);

        if (todo is null)
        {
            return null;
        }

        return new TodoDto(todo.Id, todo.Title, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt);
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodo, CancellationToken cancellationToken = default)
    {
        Todo todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = createTodo.Title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _demoApiDbContext.Todos.Add(todo);

        await _demoApiDbContext.SaveChangesAsync(cancellationToken);

        return new TodoDto(todo.Id, todo.Title, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt);
    }

    public async Task UpdateTodoAsync(Guid id, UpdateTodoDto todoToUpdate, CancellationToken cancellationToken = default)
    {
        Todo? todo = await _demoApiDbContext.Todos.FindAsync(id, cancellationToken);

        if (todo is not null)
        {
            todo.Title = todoToUpdate.Title;
            todo.IsCompleted = todoToUpdate.IsCompleted;
            todo.UpdatedAt = DateTime.UtcNow;

            await _demoApiDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Todo? todo = await _demoApiDbContext.Todos.FindAsync(id, cancellationToken);
        if (todo is not null)
        {
            _demoApiDbContext.Todos.Remove(todo);

            await _demoApiDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

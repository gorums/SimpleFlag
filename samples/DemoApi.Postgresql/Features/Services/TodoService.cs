using DemoApi.Postgresql.Domain.Entities;
using DemoApi.PostgreSQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SimpleFlag.Core;
using SimpleFlag.Core.Models;

namespace DemoApi.Postgresql.Features.Services;

public class TodoService : ITodoService
{
    private readonly DemoApiDbContext _demoApiDbContext;
    private readonly ISimpleFlagClient _simpleFlagClient;

    public TodoService(DemoApiDbContext demoApiDbContext, ISimpleFlagClient simpleFlagClient)
    {
        _demoApiDbContext = demoApiDbContext;
        _simpleFlagClient = simpleFlagClient;
    }

    public async Task<List<TodoDto>> GetTodoListAsync(CancellationToken cancellationToken = default)
    {
        var todos = new List<Todo>();

        var user = new FeatureFlagUser("todo-user");
        if (await _simpleFlagClient.GetValueAsync("get-todo", false, user, cancellationToken))
        {
            todos = await _demoApiDbContext.Todos.ToListAsync(cancellationToken);
        }

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

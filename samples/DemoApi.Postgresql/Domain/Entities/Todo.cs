namespace DemoApi.Postgresql.Domain.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

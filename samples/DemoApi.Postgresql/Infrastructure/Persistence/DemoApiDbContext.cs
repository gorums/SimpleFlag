using DemoApi.Postgresql.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.PostgreSQL.Infrastructure.Persistence;

public class DemoApiDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public DemoApiDbContext(DbContextOptions<DemoApiDbContext> options) : base(options)
    {

    }
}

using Microsoft.EntityFrameworkCore;
using TodoShared;

namespace TodoApi.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoItem> Todos { get; set; }
}

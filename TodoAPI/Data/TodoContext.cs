using Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace Todos.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> opt) : base(opt)
        {
            //Nothing
        }
        public DbSet<Todo> Todos { get; set; }
    }
}

using System.Data.Entity;
using TaskList.Core.Entities;

namespace TaskList.Core.Concrete
{
    public class TaskListDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}
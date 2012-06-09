using System.Linq;
using TaskList.Core.Entities;

namespace TaskList.Core.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskListDbContext _context = new TaskListDbContext();

        public IQueryable<Task> Products
        {
            get { return _context.Tasks; }
        }
    }
}
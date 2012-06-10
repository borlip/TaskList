using System.Collections.Generic;
using System.Data.Entity;
using TaskList.Core.Entities;
using System.Linq;

namespace TaskList.Core.Concrete
{
    /// <summary>
    /// Task Repository
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskListDbContext _context;

        public TaskRepository(TaskListDbContext context)
        {
            _context = context;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskListDbContext>());
        }

        public IEnumerable<Task> Tasks
        {
            get { return _context.Tasks.ToList(); }
        }

        public void AddNewTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
    }
}
using System.Linq;
using TaskList.Core.Entities;

namespace TaskList.Core.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskListDbContext _context;

        public TaskRepository(TaskListDbContext context)
        {
            _context = context;
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskListDbContext>());
        }

        public IQueryable<Task> Tasks
        {
            get { return _context.Tasks; }
        }

        public void AddNewTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
    }
}
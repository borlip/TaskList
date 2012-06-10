using System.Linq;
using TaskList.Core.Entities;

namespace TaskList.Core.Concrete
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        void AddNewTask(Task task);
    }
}
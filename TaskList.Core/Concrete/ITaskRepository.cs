using System.Collections.Generic;
using System.Linq;
using TaskList.Core.Entities;

namespace TaskList.Core.Concrete
{
    public interface ITaskRepository
    {
        IEnumerable<Task> Tasks { get; }
        void AddNewTask(Task task);
    }
}
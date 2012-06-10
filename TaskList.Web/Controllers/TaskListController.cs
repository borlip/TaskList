using System;
using System.Web.Mvc;
using TaskList.Core.Concrete;
using TaskList.Core.Entities;

namespace TaskList.Web.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskListController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ViewResult Index()
        {
            return View(_taskRepository.Tasks);
        }

        public ViewResult NewTask()
        {
            return View(new Task {DueDate = DateTime.Today});
        }

        [HttpPost]
        public ActionResult NewTask(Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddNewTask(task);
                TempData["message"] = string.Format("{0} has been saved", task.Name);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(task);
        }
    }
}

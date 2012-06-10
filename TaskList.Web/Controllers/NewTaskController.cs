using System;
using System.Web.Mvc;
using TaskList.Core.Concrete;
using TaskList.Core.Entities;

namespace TaskList.Web.Controllers
{
    public class NewTaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public NewTaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ViewResult Index()
        {
            return View(new Task {DueDate = DateTime.Today});
        }

        [HttpGet]
        public ViewResult Save()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Save(Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddNewTask(task);
                return View();
            }
            else
            {
                return Index();
            }
        }
    }
}

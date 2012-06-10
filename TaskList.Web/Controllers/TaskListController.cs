using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using TaskList.Core.Concrete;
using TaskList.Core.Entities;
using TaskList.Web.Helpers;
using TaskList.Web.Models;

namespace TaskList.Web.Controllers
{
    [HandleError]
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
            return View(new Task {DueDate = DateTimeUtils.Today()});
        }

        [HttpPost]
        public ActionResult NewTask(Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddNewTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Bail if we can't do anything; app will crash.
            if (filterContext == null)
                return;

            var ex = filterContext.Exception ?? new Exception("No further information exists.");
            LogException(ex);

            filterContext.ExceptionHandled = true;
            var data = new ErrorPresentation
            {
                ErrorMessage = HttpUtility.HtmlEncode(ex.Message),
                TheException = ex,
                ShowMessage = filterContext.Exception != null,
                ShowLink = false
            };
            filterContext.Result = View("Error", data);
        }

        private void LogException(Exception exception)
        {
            Trace.Write(exception);
        }
    }
}

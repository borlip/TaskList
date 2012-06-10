using System.Web.Mvc;
using TaskList.Core.Entities;

namespace TaskList.Web.Controllers
{
    public class NewTaskController : Controller
    {
        public ViewResult Index()
        {
            return View(new Task());
        }

    }
}

using System.Web.Mvc;

namespace ProjectManager.WebApp.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public ActionResult Start()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Features()
        {
            return View();
        }
    }
}
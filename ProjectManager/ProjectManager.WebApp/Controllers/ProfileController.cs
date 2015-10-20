using System.Web.Mvc;

namespace ProjectManager.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Login()
        {
            //We should redirect to controller Manager action Dashboard
            return View();
        }

        public ActionResult Register()
        {
            //We should do some mailService
            return View();
        }

        public ActionResult Logout()
        {
            //First we need loginService
            return View();
        }
    }
}
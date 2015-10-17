using ProjectManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
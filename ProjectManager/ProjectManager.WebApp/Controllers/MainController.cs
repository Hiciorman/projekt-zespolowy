using ProjectManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Controllers
{
    public class MainController : Controller
    {
        private readonly IProjectService projectService;
        public MainController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public ActionResult Start()
        {
            var model = new TestViewModel
            {
                Projects = projectService.GetAll().ToList()
            };

            return View(model);
        }
    }
}
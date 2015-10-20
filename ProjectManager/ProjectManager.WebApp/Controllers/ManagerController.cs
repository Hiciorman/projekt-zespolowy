using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.WebApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IProjectService projectService;

        public ManagerController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Projects()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KanbanBoard()
        {
            return View();
        }
    }
}
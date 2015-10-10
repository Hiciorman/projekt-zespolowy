using ProjectManager.Services.Interfaces;
using ProjectManager.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.WebApp.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProjectService _projectService;

        public DefaultController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Default
        public ActionResult Index()
        {
            var model = new TestViewModel
            {
                Projects = _projectService.GetAll()
            };

            return View(model);
        }
    }
}
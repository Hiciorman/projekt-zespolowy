using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Services.Interfaces;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IDictionaryService _dictionaryService;

        public ManagerController(IProjectService projectService, IDictionaryService dictionaryService)
        {
            this._projectService = projectService;
            this._dictionaryService = dictionaryService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KanbanBoard()
        {
            var model = new KanbanBoardViewModel
            {
                Stasuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description")
            };

            return View(model);
        }
    }
}
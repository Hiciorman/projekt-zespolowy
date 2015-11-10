using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class KanbanBoardViewModel
    {
        public IList<Assignment> Assignments { get; set; }
        public SelectList Stasuses { get; set; }
    }
}
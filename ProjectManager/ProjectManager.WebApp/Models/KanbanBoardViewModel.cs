using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class KanbanBoardViewModel
    {
        public IList<Assignment> Assignments { get; set; }
        public SelectList Stasuses { get; set; }
        public IList<User> Users { get; set; } 
        public Guid ProjectId { get; set; }
        public string CurrentSprint { get; set; }
        public List<SelectListItem> AllSprints { get; set; } 
    }
}
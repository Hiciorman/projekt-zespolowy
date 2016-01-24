using System.Web.Mvc;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class EditAssignmentViewModel
    {
        public Assignment Assignment { get; set; }
        public SelectList ListOfProjects { get; set; }
        public SelectList ListOfCategories { get; set; }
        public SelectList ListOfPriorities { get; set; }
        public SelectList ListOfUsers { get; set; }
        public SelectList ListOfStatuses { get; set; }
    }
}
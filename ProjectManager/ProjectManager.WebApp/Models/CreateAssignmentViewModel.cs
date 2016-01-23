using ProjectManager.Domain;
using System.Web.Mvc;

namespace ProjectManager.WebApp.Models
{
    public class CreateAssignmentViewModel
    {
        public Assignment Assignment { get; set; }
        public SelectList ListOfProjects { get; set; }
        public SelectList ListOfCategories { get; set; }
        public SelectList ListOfPriorities { get; set; }
        public SelectList ListOfUsers { get; set; }
        public SelectList ListOfStatuses { get; set; }
        public string OwnerName { get; set; }
    }
}
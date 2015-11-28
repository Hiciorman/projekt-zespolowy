using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class UserAssignmentsViewModel
    {
        public List<Project> Projects { get; set; }
        public List<List<Assignment>> Assignments { get; set; } 
    }
}
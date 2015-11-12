using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class UserAssignmentsViewModel
    {
        public List<Project> Projects { get; set; }
        public List<List<Assignment>> Assignments { get; set; } 
    }
}
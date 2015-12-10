using System;
using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class ProjectsViewModel
    {
        public Guid ActiveProjectId { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
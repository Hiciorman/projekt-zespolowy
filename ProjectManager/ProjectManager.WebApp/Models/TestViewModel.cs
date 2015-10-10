using ProjectManager.Domain;
using System.Collections.Generic;

namespace ProjectManager.WebApp.Models
{
    public class TestViewModel
    {
        public IList<Project> Projects { get; set; }
    }
}
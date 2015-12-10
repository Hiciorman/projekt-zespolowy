using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class DashboardViewModel
    {
        public User User { get; set; }
        public IList<Assignment> Assignments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public int BacklogCounter { get; set; }
        public int ToDoCounter { get; set; }
        public int InProgressCounter { get; set; }
        public int ReadyForReviewCounter { get; set; }
        public int DoneCounter { get; set; }




    }
}
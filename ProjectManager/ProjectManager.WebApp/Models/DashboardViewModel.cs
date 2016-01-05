using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class DashboardViewModel
    {
        public User User { get; set; }
        public IList<Assignment> Assignments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public int BacklogCount { get; set; }
        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int ReadyForReviewCount { get; set; }
        public int DoneCount { get; set; }




    }
}
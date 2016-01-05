using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class DashboardViewModel
    {
        public User User { get; set; }
        public IList<Assignment> Assignments { get; set; }
        public ICollection<Project> Projects { get; set; }

        //Status
        public int BacklogCount { get; set; }
        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int ReadyForReviewCount { get; set; }
        public int DoneCount { get; set; }

        //Priority
        public int MinorCount { get; set; }
        public int LowCount { get; set; }
        public int NormalCount { get; set; }
        public int HighCount { get; set; }
        public int CriticalCount { get; set; }

        //Category
        public int TaskCount { get; set; }
        public int BugCount { get; set; }
        public int ImprovmentCount { get; set; }

    }
}
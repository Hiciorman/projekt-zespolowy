using System.Collections.Generic;

namespace ProjectManager.Domain
{
    public class Project : GuidEntity
    {
        public Project()
        {
            Assignemnts = new HashSet<Assignment>();
            Sprints = new HashSet<Sprint>();
            Members = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Assignment> Assignemnts { get; private set; }

        public virtual ICollection<Sprint> Sprints { get; set; }

        public virtual ICollection<User> Members { get; set; }
    }
}
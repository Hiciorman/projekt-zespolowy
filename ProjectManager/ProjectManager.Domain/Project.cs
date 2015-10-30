using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Domain
{
    public class Project : GuidEntity
    {
        public Project()
        {
            Assignemnts = new HashSet<Assignment>();
            Members = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public virtual ICollection<Assignment> Assignemnts { get; set; }
        public virtual ICollection<User> Members { get; set; }
    }
}
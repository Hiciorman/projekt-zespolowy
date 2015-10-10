using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ProjectManager.Domain
{
    public class User : IdentityUser
    {
        public User()
        {
            Projects = new HashSet<Project>();
        }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

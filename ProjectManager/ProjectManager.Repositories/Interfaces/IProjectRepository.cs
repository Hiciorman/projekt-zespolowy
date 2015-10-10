using ProjectManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface IProjectService
    {
        IList<Project> GetAll();
    }
}

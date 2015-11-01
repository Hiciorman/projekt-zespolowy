using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;

namespace ProjectManager.Repositories.Interfaces
{
    public interface IDictionaryRepository
    {
        IEnumerable<Priority> GetPriorities();

        IEnumerable<Status> GetStatus();

        IEnumerable<Category> GetCategory();

        IEnumerable<User> GetUser();
    }
}

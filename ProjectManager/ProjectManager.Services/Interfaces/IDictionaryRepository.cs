using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface IDictionaryRepository
    {
        IList<Priority> GetPriorities();
        IList<Status> GetStatuses();
        IList<Category> GetCategories();
    }
}
using System;
using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.Repositories.Interfaces
{
    public interface IDictionaryRepository
    {
        IEnumerable<Priority> GetPriorities();

        IEnumerable<Status> GetStatuses();

        IEnumerable<Category> GetCategories();

        IEnumerable<User> GetUsers(Guid id);
    }
}

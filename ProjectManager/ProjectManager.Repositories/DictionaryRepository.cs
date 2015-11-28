using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;

namespace ProjectManager.Repositories
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly AppContext _context;

        public DictionaryRepository(AppContext context)
        {
            this._context = context;
        }
        public IEnumerable<Priority> GetPriorities()
        {
            return _context.Priorities;
        }

        public IEnumerable<Status> GetStatuses()
        {
            return _context.Statuses;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }
    }
}

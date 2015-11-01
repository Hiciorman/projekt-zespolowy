using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        public DictionaryService(IDictionaryRepository dictionaryRepository)
        {
            this._dictionaryRepository = dictionaryRepository;
        }
        public IList<Priority> GetPriorities()
        {
            return _dictionaryRepository.GetPriorities().ToList();
        }

        public IList<Status> GetStatuses()
        {
            return _dictionaryRepository.GetStatus().ToList();
        }

        public IList<Category> GetCategories()
        {
            return _dictionaryRepository.GetCategory().ToList();
        }

        public IList<User> GetUsers()
        {
            return _dictionaryRepository.GetUser().ToList();
        }
    }
}

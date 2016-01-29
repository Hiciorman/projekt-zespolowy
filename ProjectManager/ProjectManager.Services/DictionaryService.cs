using System;
using System.Collections.Generic;
using System.Linq;
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
            return _dictionaryRepository.GetStatuses().ToList();
        }

        public IList<Category> GetCategories()
        {
            return _dictionaryRepository.GetCategories().ToList();
        }

        public IList<User> GetUsers(Guid id)
        {
            return _dictionaryRepository.GetUsers(id).ToList();
        }
    }
}

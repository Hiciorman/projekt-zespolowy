using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        public DictionaryRepository(IDictionaryRepository dictionaryRepository)
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
    }
}

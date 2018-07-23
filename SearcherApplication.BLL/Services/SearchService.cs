using SearcherApplication.BLL.Interfaces;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public Task<List<SearchResult>> GetSearchResults(string query)
        {
            var results = _searchRepository.GetSearchResults(query);
            return results;
        }
    }
}
using SearcherApplication.BLL.Interfaces;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SearcherApplication.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        private readonly ISearcherFactory _searcherFactory;

        public SearchService(ISearchRepository searchRepository,
            ISearcherFactory searcherFactory)
        {
            _searchRepository = searchRepository;
            _searcherFactory = searcherFactory;
        }

        public async Task<List<SearchResult>> GetSearchResultsAsync(string query)
        {
            var googleSearchEnabled = bool.Parse(ConfigurationManager.AppSettings["GoogleSearchEnabled"]);
            var bingSearchEnabled = bool.Parse(ConfigurationManager.AppSettings["BingSearchEnabled"]);

            var searchTasks = new List<Task<List<SearchResult>>>();

            var registerTask = new Action<bool, ISearcher>(
                (flag, searcher) =>
                {
                    if (flag && searcher != null)
                    {
                        searchTasks.Add(searcher.GetSearchResultsAsync(query));
                    }
                });

            ISearcher googleSearcher = _searcherFactory.CreateGoogleSearcher();
            ISearcher bingSearcher = _searcherFactory.CreateBingSearcher();

            registerTask(googleSearchEnabled, googleSearcher);
            registerTask(bingSearchEnabled, bingSearcher);

            Task<List<SearchResult>> firstExecutedTask = await Task.WhenAny(searchTasks);
            List<SearchResult> searchResults = await firstExecutedTask;

            //TEST GITHUB DESKTOP
            if (searchResults?.Count() != 0)
            {
                _searchRepository.AddSearchResults(searchResults, query);
            }

            return searchResults;
        }

        public IEnumerable<SearchQuery> GetAllSearchQueries()
        {
            return _searchRepository.GetAllSearchQueries();
        }

        public IEnumerable<SearchResult> GetSearchResultsByQueryId(int searchQueryId)
        {
            return _searchRepository.GetSearchResultsByQueryId(searchQueryId);
        }
    }
}
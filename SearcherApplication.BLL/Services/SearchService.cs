using SearcherApplication.BLL.Interfaces;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.SearcherFactory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace SearcherApplication.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        private readonly AbstractSearcherFactory _searcherFactory;

        public SearchService(ISearchRepository searchRepository,
            AbstractSearcherFactory searcherFactory)
        {
            _searchRepository = searchRepository;
            _searcherFactory = searcherFactory;
        }

        public async Task<List<SearchResult>> GetSearchResultsAsync(string query)
        {
            var googleSearchEnabled = true;//bool.Parse(ConfigurationManager.AppSettings["sdsd"]); web config <appsettings> TODO LATER
            var bingSearchEnabled = true;
            //var yahooSearchEnabled = true;

            var searchTasks = new List<Task<List<SearchResult>>>();

            var registerTask = new Action<bool, ISearcher>(
                (flag, searcher) =>
                {
                    if (flag)
                    {
                        searchTasks.Add(searcher.GetSearchResultsAsync(query));
                    }
                });

            ISearcher googleSearcher = _searcherFactory.CreateGoogleSearcher();
            ISearcher bingSearcher = _searcherFactory.CreateBingSearcher();
            //ISearcher yahooSearcher = new YahooSearcher(yahooSearchSystem.ApiKey);

            registerTask(googleSearchEnabled, googleSearcher);
            registerTask(bingSearchEnabled, bingSearcher);
            //registerTask(yahooSearchEnabled, yahooSearcher);

            var firstExecutedTask = await Task.WhenAny(searchTasks);
            List<SearchResult> searchResults = await firstExecutedTask;

            if(searchResults != null)
            {
                _searchRepository.AddSearchResults(searchResults, query);
            }
            
            return searchResults;
        }

        public List<SearchQuery> GetSearchQueries()
        {
            return _searchRepository.GetSearchQueries();
        }

        public List<SearchResult> GetSearchResultsByQuery(int searchQueryId)
        {
            return _searchRepository.GetSearchResultsByQuery(searchQueryId);
        }
    }
}
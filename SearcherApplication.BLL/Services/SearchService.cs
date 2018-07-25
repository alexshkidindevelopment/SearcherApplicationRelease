using Newtonsoft.Json;
using SearcherApplication.BLL.Interfaces;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;
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
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<List<SearchResult>> GetSearchResults(string query)
        {
            string searchSystemsJson = ConfigurationManager.AppSettings["SearchSystems"];
            List<SearchEngineSettings> searchSystems = JsonConvert.DeserializeObject<List<SearchEngineSettings>>(searchSystemsJson);

            var googleSearchEnabled = true;//bool.Parse(ConfigurationManager.AppSettings["sdsd"]); web config <appsettings> TODO LATER
            var bingSearchEnabled = true;
            //var yahooSearchEnabled = true;

            var searchTasks = new List<Task<List<SearchResult>>>();

            var registerTask = new Action<bool, ISearcher>(
                (flag, searcher) =>
                {
                    if (flag)
                    {
                        searchTasks.Add(searcher.GetSearchResults(query));
                    }
                });

            SearchEngineSettings googleSearchSystem = searchSystems.Where(s => s.Name == "Google").First();
            SearchEngineSettings bingSearchSystem = searchSystems.Where(s => s.Name == "Bing").First();
            //SearchEngineSettings yahooSearchSystem = searchSystems.Where(s => s.Name == "Yahoo").First();

            ISearcher googleSearcher = new GoogleSearcher(googleSearchSystem.ApiKey, googleSearchSystem.SearchEngineId);
            ISearcher bingSearcher = new BingSearcher(bingSearchSystem.ApiKey);
            //ISearcher yahooSearcher = new YahooSearcher(yahooSearchSystem.ApiKey);

            registerTask(googleSearchEnabled, googleSearcher);
            registerTask(bingSearchEnabled, bingSearcher);
            //registerTask(yahooSearchEnabled, yahooSearcher);

            var firstExecutedTask = await Task.WhenAny(searchTasks);
            List<SearchResult> searchResults = await firstExecutedTask;

            if(searchResults == null)
            {
                return null;
            }

            _searchRepository.AddSearchResults(searchResults, query);

            return searchResults;
        }
    }
}
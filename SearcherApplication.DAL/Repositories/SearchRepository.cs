using Newtonsoft.Json;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.DAL.Searchers;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SearcherApplication.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchHistoryStorageContext _context;

        public SearchRepository()
        {
            _context = new SearchHistoryStorageContext();
        }

        async public Task<List<SearchResult>> GetSearchResults(string query)
        {
            string parameter = ConfigurationManager.AppSettings["SearchSystems"];
            List<SearchEngineSettings> searchSystems = JsonConvert.DeserializeObject<List<SearchEngineSettings>>(parameter);

            IEnumerable<Task<List<SearchResult>>> tasks = searchSystems.Select(s =>
            {
                return ProcessSearchRequest(s, query);
            });
            List<Task<List<SearchResult>>> downloadTasks = tasks.ToList();
            
            while (downloadTasks.Count > 0)
            {
                Task<List<SearchResult>> firstFinishedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(firstFinishedTask);
                List<SearchResult> results = await firstFinishedTask;
            }

            return null;
        }

        async Task<List<SearchResult>> ProcessSearchRequest(SearchEngineSettings system, string query)
        {
            SearchExecutionContext searchExecutionContext;
            List<SearchResult> results = new List<SearchResult>();
            if (system.Name == "Google")
            {
                searchExecutionContext = new SearchExecutionContext(new GoogleSearcher());
                Task<List<SearchResult>> myTask =
                    Task.Run(() => searchExecutionContext.ExecuteSearch(query, system.ApiKey, system.SearchEngineId));
                results = await myTask;
            }
            else if (system.Name == "Yahoo")
            {
                searchExecutionContext = new SearchExecutionContext(new YahooSearcher());
                Task<List<SearchResult>> myTask =
                    Task.Run(() => searchExecutionContext.ExecuteSearch(query, system.ApiKey, system.SearchEngineId));
                results = await myTask;
            }
            else if (system.Name == "Bing")
            {
                searchExecutionContext = new SearchExecutionContext(new BingSearcher());
                Task<List<SearchResult>> myTask =
                    Task.Run(() => searchExecutionContext.ExecuteSearch(query, system.ApiKey, system.SearchEngineId));
                results = await myTask;
            }
            return results;
        }
    }
}
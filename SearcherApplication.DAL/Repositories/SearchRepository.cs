using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.DAL.Searchers;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearcherApplication.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private SearchContext _context;

        public SearchRepository()
        {
            _context = new SearchContext();
        }

        async public Task<List<SearchResult>> GetSearchResults(string query)
        {
            List<SearchSystem> searchSystems = _context.SearchSystems.ToList();
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

        async Task<List<SearchResult>> ProcessSearchRequest(SearchSystem system, string query)
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
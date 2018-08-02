using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace SearcherApplication.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchHistoryStorageContext _context;

        public SearchRepository(SearchHistoryStorageContext context)
        {
            _context = context;
        }

        public void AddSearchResults(List<SearchResult> results, string queryText)
        {
            if (results?.Count == 0 || string.IsNullOrEmpty(queryText))
            {
                return;
            }

            var searchQuery = new SearchQuery
            {
                QueryText = queryText
            };

            _context.SearchQueries.Add(searchQuery);
            results.ForEach(c => c.SearchQuery = searchQuery);
            _context.SearchResults.AddRange(results);
            _context.SaveChanges();
        }

        public IEnumerable<SearchQuery> GetAllSearchQueries()
        {
            return _context.SearchQueries.ToList();
        }

        public IEnumerable<SearchResult> GetSearchResultsByQueryId(int searchQueryId)
        {
            if (searchQueryId < 1)
            {
                return null;
            }

            var searchResults = _context.SearchResults.Where(r => r.SearchQueryId == searchQueryId).ToList();

            return searchResults;
        }
    }
}
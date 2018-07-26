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

        public SearchRepository()
        {
            _context = new SearchHistoryStorageContext();
        }

        public void AddSearchResults(List<SearchResult> results, string query)
        {
            var searchQuery = new SearchQuery
            {
                QueryText = query
            };

            _context.SearchQueries.Add(searchQuery);
            results = results.Select(c => { c.SearchQuery = searchQuery; return c; }).ToList();
            _context.SearchedResults.AddRange(results);
            _context.SaveChanges();
        }
    }
}
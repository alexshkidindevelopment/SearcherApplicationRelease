using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherApplication.SearchEngine.Searchers
{
    public class SearchExecutionContext
    {
        private readonly ISearcher _contextSearcher;

        public SearchExecutionContext(ISearcher _searcher)
        {
            _contextSearcher = _searcher;
        }

        async public Task<List<SearchResult>> ExecuteSearch(string query)
        {
            List<SearchResult> results = await _contextSearcher.GetSearchResults(query);
            return results;
        }
    }
}
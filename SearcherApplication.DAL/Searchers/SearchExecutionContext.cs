using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherApplication.DAL.Searchers
{
    public class SearchExecutionContext
    {
        private readonly ISearcher _contextSearcher;

        public SearchExecutionContext(ISearcher _searcher)
        {
            _contextSearcher = _searcher;
        }

        public List<SearchResult> ExecuteSearch(string query, string apiKey, string searchEngineId)
        {
            List<SearchResult> results = _contextSearcher.GetSearchResults(query, apiKey, searchEngineId);
            return results;
        }
    }
}
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
        public ISearcher ContextSearcher { get; set; }

        public SearchExecutionContext(ISearcher _searcher)
        {
            ContextSearcher = _searcher;
        }

        public List<SearchResult> ExecuteSearch(string query, string apiKey, string searchEngineId)
        {
            List<SearchResult> results = ContextSearcher.StartSearch(query, apiKey, searchEngineId);
            return results;
        }
    }
}
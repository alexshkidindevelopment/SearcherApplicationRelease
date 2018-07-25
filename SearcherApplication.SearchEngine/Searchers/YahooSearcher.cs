using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.SearchEngine.Searchers
{
    public class YahooSearcher : ISearcher
    {
        private readonly string _apiKey;

        public YahooSearcher(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Task<List<SearchResult>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
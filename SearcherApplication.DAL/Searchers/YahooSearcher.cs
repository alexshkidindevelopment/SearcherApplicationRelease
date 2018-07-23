using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Searchers
{
    public class YahooSearcher : ISearcher
    {
        public List<SearchResult> StartSearch(string query, string apiKey, string searchEngineId)
        {
            throw new NotImplementedException();
        }
    }
}
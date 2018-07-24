﻿using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearcherApplication.SearchEngine.Searchers
{
    public class GoogleSearcher : ISearcher
    {
        private readonly string _apiKey;

        private readonly string _searchEngineId;

        private readonly int _numberOfPage = 1;

        public GoogleSearcher(string apiKey, string searchEngineId)
        {
            _apiKey = apiKey;
            _searchEngineId = searchEngineId;
        }

        public async Task<List<SearchResult>> GetSearchResults(string query)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = _searchEngineId;
            listRequest.Start = _numberOfPage;
            return await Task.Run(() => { return Map(listRequest.Execute()); });
        }

        private List<SearchResult> Map(Search search)
        {
            List<SearchResult> results = new List<SearchResult>();
            foreach (Result result in search.Items)
            {
                SearchResult tempResult = new SearchResult
                {
                    Title = result.Title,
                    Link = result.Link
                };
                results.Add(tempResult);
            }

            return results;
        }
    }
}
﻿using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Google.Apis.Customsearch.v1.CseResource;

namespace SearcherApplication.SearchEngine.Searchers
{
    public class GoogleSearcher : ISearcher
    {
        private readonly string _apiKey;

        private readonly string _searchEngineId;

        private const int _numberOfPage = 1;

        public GoogleSearcher(string apiKey, string searchEngineId)
        {
            _apiKey = apiKey;
            _searchEngineId = searchEngineId;
        }

        public async virtual Task<List<SearchResult>> GetSearchResultsAsync(string query)
        {
            //TODO: Need to add a logger, NLog for example
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return null;
                }

                ListRequest listRequest = CreateListRequest(query);

                return await Task.Run(() => { return Map(listRequest.Execute()); });
            }
            catch(Exception ex)
            {
                await Task.Delay(10000);
                return null;
            }
        }

        private List<SearchResult> Map(Search search)
        {
            if (search?.Items == null)
            {
                return null;
            }

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

        private ListRequest CreateListRequest(string query)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _apiKey });
            ListRequest listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = _searchEngineId;
            listRequest.Start = _numberOfPage;

            return listRequest;
        }
    }
}
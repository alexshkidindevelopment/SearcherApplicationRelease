using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace SearcherApplication.DAL.Searchers
{
    public class GoogleSearcher : ISearcher
    {
        private int _numberOfPage = 1;

        public List<SearchResult> GetSearchResults(string query, string apiKey, string searchEngineId)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = searchEngineId;
            listRequest.Start = _numberOfPage;
            List<Result> unmappedResults = listRequest.Execute().Items.ToList();

            List<SearchResult> results = new List<SearchResult>();
            foreach (Result result in unmappedResults)
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
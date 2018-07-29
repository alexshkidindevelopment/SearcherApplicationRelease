using SearcherApplication.Models.DataModels;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Interfaces
{
    public interface ISearchRepository
    {
        void AddSearchResults(List<SearchResult> results, string queryText);
        List<SearchQuery> GetSearchQueries();
        List<SearchResult> GetSearchResultsByQuery(int searchQueryId);
    }
}
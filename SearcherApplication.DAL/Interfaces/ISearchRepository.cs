using SearcherApplication.Models.DataModels;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Interfaces
{
    public interface ISearchRepository
    {
        void AddSearchResults(List<SearchResult> results, string queryText);
        IEnumerable<SearchQuery> GetAllSearchQueries();
        IEnumerable<SearchResult> GetSearchResultsByQueryId(int searchQueryId);
    }
}
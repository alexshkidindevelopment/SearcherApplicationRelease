using SearcherApplication.Models.DataModels;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Interfaces
{
    public interface ISearcher
    {
        List<SearchResult> GetSearchResults(string query, string apiKey, string searchEngineId);
    }
}
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.SearchEngine.Interfaces
{
    public interface ISearcher
    {
        Task<List<SearchResult>> GetSearchResults(string query);
    }
}
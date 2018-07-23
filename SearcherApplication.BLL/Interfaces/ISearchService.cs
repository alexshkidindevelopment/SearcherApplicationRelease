using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.BLL.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchResult>> GetSearchResults(string query);
    }
}
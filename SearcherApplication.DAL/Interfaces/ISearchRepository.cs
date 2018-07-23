using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.DAL.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<SearchResult>> GetSearchResults(string query);
    }
}
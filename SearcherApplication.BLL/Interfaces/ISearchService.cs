﻿using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearcherApplication.BLL.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchResult>> GetSearchResultsAsync(string query);

        IEnumerable<SearchQuery> GetAllSearchQueries();

        IEnumerable<SearchResult> GetSearchResultsByQueryId(int searchQueryId);
    }
}
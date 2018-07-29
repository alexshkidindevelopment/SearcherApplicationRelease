﻿using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearcherApplication.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchHistoryStorageContext _context;

        public SearchRepository()
        {
            _context = new SearchHistoryStorageContext();
        }

        public void AddSearchResults(List<SearchResult> results, string queryText)
        {
            var searchQuery = new SearchQuery
            {
                QueryText = queryText
            };

            _context.SearchQueries.Add(searchQuery);
            results = results.Select(c => { c.SearchQuery = searchQuery; return c; }).ToList();
            _context.SearchResults.AddRange(results);
            _context.SaveChanges();
        }

        public List<SearchQuery> GetSearchQueries()
        {
            return _context.SearchQueries.ToList();
        }

        public List<SearchResult> GetSearchResultsByQuery(int searchQueryId)
        {
            var searchResults = _context.SearchResults.Where(r => r.SearchQueryId == searchQueryId).ToList();
            return searchResults;
        }
    }
}
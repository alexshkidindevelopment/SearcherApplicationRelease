﻿using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using System;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Searchers
{
    public class BingSearcher : ISearcher
    {
        public List<SearchResult> GetSearchResults(string query, string apiKey, string searchEngineId)
        {
            throw new NotImplementedException();
        }
    }
}
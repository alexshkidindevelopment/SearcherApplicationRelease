using FakeItEasy;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Data.Entity;

namespace SearcherApplication.DAL.Tests.Helpers
{
    public class ContextMockHelper
    {
        //public List<SearchQuery> SearchQueries = new List<SearchQuery>();

        //public List<SearchResult> SearchResults = new List<SearchResult>();

        private SearchHistoryStorageContext _fakeContext;

        public SearchHistoryStorageContext FakeContext
        {
            get
            {
                return _fakeContext;
            }
            set
            {
                _fakeContext = value;
            }
        }

        public ContextMockHelper()
        {
            _fakeContext = A.Fake<SearchHistoryStorageContext>();
        }

        public ContextMockHelper Build<T>(IEnumerable<T> list) where T: class
        {
            switch (typeof(T).Name)
            {
                case nameof(SearchQuery):
                    A.CallTo(() => _fakeContext.SearchQueries)
                        .Returns(DbSetMockHelper.GetMockDbSet<SearchQuery>((IEnumerable<SearchQuery>) list));
                    break;
                case nameof(SearchResult):
                    A.CallTo(() => _fakeContext.SearchResults)
                        .Returns(DbSetMockHelper.GetMockDbSet<SearchResult>((IEnumerable<SearchResult>)list));
                    break;
                default:
                    throw new KeyNotFoundException(nameof(T));
            }
            
            return this;
        }
    }
}
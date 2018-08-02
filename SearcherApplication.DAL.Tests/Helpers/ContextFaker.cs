using FakeItEasy;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;

namespace SearcherApplication.DAL.Tests.Helpers
{
    public class ContextFaker
    {
        public List<SearchQuery> SearchQueries = new List<SearchQuery>();

        public List<SearchResult> SearchResults = new List<SearchResult>();

        private SearchHistoryStorageContext _fakeContext;

        public SearchHistoryStorageContext FakeContext
        {
            get
            {
                A.CallTo(() => _fakeContext.SearchQueries).Returns(ListFaker<SearchQuery>.GetFake(SearchQueries));
                A.CallTo(() => _fakeContext.SearchResults).Returns(ListFaker<SearchResult>.GetFake(SearchResults));
                return _fakeContext;
            }
            set
            {
                _fakeContext = value;
            }
        }

        public ContextFaker()
        {
            _fakeContext = A.Fake<SearchHistoryStorageContext>();
        }
    }
}
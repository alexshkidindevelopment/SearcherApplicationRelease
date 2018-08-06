using FakeItEasy;
using NUnit.Framework;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Repositories;
using SearcherApplication.DAL.Tests.Helpers;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace SearcherApplication.DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class SearchRepositoryTests
    {
        private SearchRepository _searchRepository;
        private SearchHistoryStorageContext _mockContext;

        [SetUp]
        public void Initialize()
        {
            _mockContext = A.Fake<SearchHistoryStorageContext>();

            _searchRepository = new SearchRepository(_mockContext);
        }

        [Test]
        public void GetAllSearchQueries_SearchQueriesExist_ReturnsListOfSearchQueries()
        {
            //Arrange
            int notExpectedCount = 0;

            var listOfQueries = new List<SearchQuery>();
            listOfQueries.Add(new SearchQuery
            {
                Id = 222,
                QueryText = "Skateboard"
            });

            var contextMockHelper = new ContextMockHelper()
                .Build<SearchQuery>(listOfQueries);

            _searchRepository = new SearchRepository(contextMockHelper.FakeContext);

            //Act
            IEnumerable<SearchQuery> result = _searchRepository.GetAllSearchQueries();

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchQuery>>(result);
            Assert.AreNotEqual(result?.Count(), notExpectedCount);
        }

        [Test]
        public void GetSearchResultsByQueryId_SearchQueryIdExisted_ReturnsListOfSearchResults()
        {
            //Arrange
            int searchQueryId = 222;
            int notExpectedCount = 0;

            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world",
                SearchQueryId = 222
            });

            var listOfQueries = new List<SearchQuery>();
            listOfQueries.Add(new SearchQuery
            {
                Id = 222,
                QueryText = "Skateboard"
            });

            var contextMockHelper = new ContextMockHelper()
                .Build<SearchQuery>(listOfQueries)
                .Build<SearchResult>(listOfResults);

            _searchRepository = new SearchRepository(contextMockHelper.FakeContext);

            //Act
            IEnumerable<SearchResult> result = _searchRepository.GetSearchResultsByQueryId(searchQueryId);

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchResult>>(result);
            Assert.AreNotEqual(result?.Count(), notExpectedCount);
        }

        [Test]
        public void GetSearchResultsByQueryId_SearchQueryIdLesserThanOne_ReturnsNull()
        {
            //Arrange
            int searchQueryId = 0;
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world",
                SearchQueryId = 222
            });

            var listOfQueries = new List<SearchQuery>();
            listOfQueries.Add(new SearchQuery
            {
                Id = 222,
                QueryText = "Skateboard"
            });

            var contextMockHelper = new ContextMockHelper()
                .Build<SearchQuery>(listOfQueries)
                .Build<SearchResult>(listOfResults);

            _searchRepository = new SearchRepository(contextMockHelper.FakeContext);

            //Act
            IEnumerable<SearchResult> result = _searchRepository.GetSearchResultsByQueryId(searchQueryId);

            //Assert
            Assert.IsNull(result);
        }

        //[Test]
        //public void SearchRepository_AddSearchResults_AddedSearchResults()
        //{
            //Arrange
            //var searchQueryText = "Skateboard";


            //Act
            //_searchRepository.AddSearchResults(listOfResults, searchQueryText);

        //}
    }
}
using FakeItEasy;
using NUnit.Framework;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Repositories;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace SearcherApplication.DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class SearchRepositoryTests
    {
        private SearchHistoryStorageContext _context;

        private SearchRepository _searchRepository;

        [SetUp]
        public void Initialize()
        {
            _context = A.Fake<SearchHistoryStorageContext>();
            _searchRepository = new SearchRepository();
        }

        [Test]
        public void SearchRepository_GetAllSearchQueries_ReturnsListOfSearchQueries()
        {
            //Act
            IEnumerable<SearchQuery> result = _searchRepository.GetAllSearchQueries();

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchQuery>>(result);
        }

        [Test]
        public void SearchRepository_GetSearchResultsByQueryId_ReturnsListOfSearchResults()
        {
            //Arrange
            int searchQueryId = 4;
            int notExpectedCount = 0;

            //Act
            IEnumerable<SearchResult> result = _searchRepository.GetSearchResultsByQueryId(searchQueryId);

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchResult>>(result);
            Assert.AreNotEqual(result.Count(), notExpectedCount);
        }

        [Test]
        public void SearchRepository_GetSearchResultsByQueryId_ReturnsNullWhenQueryIdIsLesserThanOne()
        {
            //Arrange
            int searchQueryId = 0;

            //Act
            IEnumerable<SearchResult> result = _searchRepository.GetSearchResultsByQueryId(searchQueryId);

            //Assert
            Assert.IsNull(result);
        }
    }
}
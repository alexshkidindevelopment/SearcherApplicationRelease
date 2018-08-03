using FakeItEasy;
using NUnit.Framework;
using SearcherApplication.DAL.Infrastructure;
using SearcherApplication.DAL.Repositories;
using SearcherApplication.DAL.Tests.Helpers;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SearcherApplication.DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class SearchRepositoryTests
    {
        private SearchRepository _searchRepository;

        [SetUp]
        public void Initialize()
        {
            var contextFaker = new ContextFaker();
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world",
                SearchQueryId = 1111
            });

            var listOfQueries = new List<SearchQuery>();
            listOfQueries.Add(new SearchQuery
            {
                Id = 222,
                QueryText = "Skateboard"
            });

            contextFaker.SearchResults.AddRange(listOfResults);
            contextFaker.SearchQueries.AddRange(listOfQueries);

            _searchRepository = new SearchRepository(contextFaker.FakeContext);
        }

        [Test]
        public void SearchRepository_GetAllSearchQueries_ReturnsListOfSearchQueries()
        {
            //Arrange
            int notExpectedCount = 0;
            
            //Act
            IEnumerable<SearchQuery> result = _searchRepository.GetAllSearchQueries();

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchQuery>>(result);
            Assert.AreNotEqual(result?.Count(), notExpectedCount);
        }

        [Test]
        public void SearchRepository_GetSearchResultsByQueryId_ReturnsListOfSearchResults()
        {
            //Arrange
            int searchQueryId = 222;
            int notExpectedCount = 0;
            
            //Act
            IEnumerable<SearchResult> result = _searchRepository.GetSearchResultsByQueryId(searchQueryId);

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchResult>>(result);
            Assert.AreNotEqual(result?.Count(), notExpectedCount);
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
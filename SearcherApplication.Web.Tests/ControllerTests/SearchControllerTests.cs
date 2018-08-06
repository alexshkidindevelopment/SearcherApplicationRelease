using SearcherApplication.BLL.Interfaces;
using SearcherApplication.Web.Controllers;
using NUnit.Framework;
using FakeItEasy;
using System.Web.Mvc;
using System.Threading.Tasks;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;

namespace SearcherApplication.Web.Tests
{
    [TestFixture]
    public class SearchControllerTests
    {
        private SearchController _searchController;

        private ISearchService _searchService;

        [SetUp]
        public void Initialize()
        {
            _searchService = A.Fake<ISearchService>();
            _searchController = new SearchController(_searchService);
        }

        [Test]
        public void GetSearchResults_QueryIsValid_ReturnsActionResult()
        {
            //Arrange
            var query = "Skateboard";

            //Act
            var result = _searchController.GetSearchResults(query);

            //Assert
            Assert.IsInstanceOf<Task<ActionResult>>(result);
        }

        [Test]
        public void GetSearchResults_QueryIsValidAndSearchResultsExists_ViewIsGetSearchResults()
        {
            //Arrange
            var query = "Skateboard";
            var expectedResult = "GetSearchResults";
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });
            A.CallTo(() => _searchService.GetSearchResultsAsync(query))
                .Returns(listOfResults);

            //Act
            var result = _searchController.GetSearchResults(query).Result as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetSearchResults_QueryIsValidAndSearchResultsNotExists_ViewIsEmptySearch()
        {
            //Arrange
            var query = "Skateboard";
            var expectedResult = "EmptySearch";
            A.CallTo(() => _searchService.GetSearchResultsAsync(query))
                .Returns(new List<SearchResult>());

            //Act
            var result = _searchController.GetSearchResults(query).Result as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetSearchResults_QueryIsNullOrEmpty_ViewIsEmptySearch()
        {
            //Arrange
            var query = "";
            var expectedResult = "EmptySearch";
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });
            A.CallTo(() => _searchService.GetSearchResultsAsync(query))
                .Returns(listOfResults);

            //Act
            var result = _searchController.GetSearchResults(query).Result as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetSearchHistory_SearchQueriesExists_ReturnsActionResult()
        {
            //Act
            var result = _searchController.GetSearchHistory() as ViewResult;

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void GetSearchHistory_SearchQueriesExists_ViewIsGetSearchHistory()
        {
            //Arrange
            var expectedResult = "GetSearchHistory";
            var listOfResults = new List<SearchQuery>();
            listOfResults.Add(new SearchQuery
            {
                Id = 1111,
                QueryText = "Skateboard"
            });
            A.CallTo(() => _searchService.GetAllSearchQueries())
                .Returns(listOfResults);

            //Act
            var result = _searchController.GetSearchHistory() as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetSearchHistory_SearchQueriesNotExists_ViewIsEmptySearch()
        {
            //Arrange
            var expectedResult = "EmptySearch";
            A.CallTo(() => _searchService.GetAllSearchQueries())
                .Returns(new List<SearchQuery>());

            //Act
            var result = _searchController.GetSearchHistory() as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetQueryResultsById_QueryIdIsValid_ReturnsActionResult()
        {
            //Arrange
            int? id = 4;

            //Act
            var result = _searchController.GetQueryResultsById(id) as ViewResult;

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void GetQueryResultsById_QueryIdIsValidAndSearchResultsExist_ViewIsGetSearchResults()
        {
            //Arrange
            int? id = 4;
            var expectedResult = "GetSearchResults";
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => _searchService.GetSearchResultsByQueryId(id.Value))
                .Returns(listOfResults);

            //Act
            var result = _searchController.GetQueryResultsById(id) as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetQueryResultsById_QueryIdIsValidAndSearchResultsNotExist_ViewIsEmptySearch()
        {
            //Arrange
            int? id = 4;
            var expectedResult = "EmptySearch";
            A.CallTo(() => _searchService.GetSearchResultsByQueryId(id.Value))
                .Returns(new List<SearchResult>());

            //Act
            var result = _searchController.GetQueryResultsById(id) as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void GetQueryResultsById_QueryIdIsNotValid_ViewIsEmptySearch()
        {
            //Arrange
            int? id = null;
            var expectedResult = "EmptySearch";

            //Act
            var result = _searchController.GetQueryResultsById(id) as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }
    }
}
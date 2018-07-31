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
        public void SearchController_GetSearchResults_ReturnsActionResult()
        {
            //Arrange
            var query = "Skateboard";

            //Act
            var result = _searchController.GetSearchResults(query);

            //Assert
            Assert.IsInstanceOf<Task<ActionResult>>(result);
        }

        [Test]
        public void SearchController_GetSearchResults_ViewIsGetSearchResultsWhenResultsNotEmpty()
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
        public void SearchController_GetSearchResults_ViewIsEmptySearchWhenResultsEmpty()
        {
            //Arrange
            var query = "Skateboard";
            var expectedResult = "EmptySearch";

            //Act
            var result = _searchController.GetSearchResults(query).Result as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void SearchController_GetSearchHistory_ReturnsActionResult()
        {
            //Act
            var result = _searchController.GetSearchHistory();

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void SearchController_GetSearchHistory_ViewIsGetSearchHistoryWhenResultsNotEmpty()
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
        public void SearchController_GetSearchHistory_ViewIsEmptySearchWhenResultsEmpty()
        {
            //Arrange
            var expectedResult = "EmptySearch";

            //Act
            var result = _searchController.GetSearchHistory() as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void SearchController_ViewQueryResults_ReturnsActionResult()
        {
            //Arrange
            int? id = 4;

            //Act
            var result = _searchController.ViewQueryResults(id);

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void SearchController_ViewQueryResults_ViewIsGetSearchResultsWhenResultsNotEmpty()
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
            var result = _searchController.ViewQueryResults(id) as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }

        [Test]
        public void SearchController_ViewQueryResults_ViewIsEmptySearchWhenResultsEmpty()
        {
            //Arrange
            int? id = 4;
            var expectedResult = "EmptySearch";

            //Act
            var result = _searchController.ViewQueryResults(id) as ViewResult;

            //Assert
            Assert.AreEqual(expectedResult, result.ViewName);
        }
    }
}
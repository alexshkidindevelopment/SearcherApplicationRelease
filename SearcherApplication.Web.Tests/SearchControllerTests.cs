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
        public void SearchController_GetSearchResults_ViewIsGetSearchResults()
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
    }
}
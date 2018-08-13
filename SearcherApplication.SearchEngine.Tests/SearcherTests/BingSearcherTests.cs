using FakeItEasy;
using NUnit.Framework;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Searchers;
using System.Collections.Generic;
using System.Configuration;

namespace SearcherApplication.SearchEngine.Tests.SearcherTests
{
    [TestFixture]
    public class BingSearcherTests
    {
        private BingSearcher bingSearcher;
        private readonly string _bingUrl = ConfigurationManager.AppSettings["BingApiUrl"];

        [SetUp]
        public void Initialize()
        {
            bingSearcher = A.Fake<BingSearcher>();
        }

        [Test]
        public void GetSearchResultsAsync_QueryIsValid_ReturnsListOfSearchResults()
        {
            //Arrange
            var query = "Skateboard";
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world",
                SearchQueryId = 222
            });

            //Act
            var result = bingSearcher.GetSearchResultsAsync(query).Result;

            //Assert
            Assert.IsInstanceOf<List<SearchResult>>(result);
        }
    }
}
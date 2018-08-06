using FakeItEasy;
using NUnit.Framework;
using SearcherApplication.BLL.Interfaces;
using SearcherApplication.BLL.Services;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;
using System.Collections.Generic;
using System.Configuration;

namespace SearcherApplication.BLL.Tests.ServiceTests
{
    [TestFixture]
    public class SearchServiceTests
    {
        private ISearchService _searchService;

        private ISearchRepository _searchRepository;

        private ISearcherFactory _searcherFactory;

        [SetUp]
        public void Initialize()
        {
            _searchRepository = A.Fake<ISearchRepository>();
            _searcherFactory = A.Fake<ISearcherFactory>();
            _searchService = new SearchService(_searchRepository, _searcherFactory);
        }

        //TODO: Need a reworking
        [Test]
        public void GetSearchResultsAsync_QueryIsValid_ReturnsListOfSearchResults()
        {
            //Arrange
            var query = "Skateboard";
            var fakeGoogleSearcher = A.Fake<GoogleSearcher>();
            var fakeBingSearcher = A.Fake<BingSearcher>();
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => fakeGoogleSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);
            A.CallTo(() => fakeBingSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);

            int notExpectedResultCount = 0;
            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "true";
            ConfigurationManager.AppSettings["BingSearchEnabled"] = "true";
            ConfigurationManager.AppSettings["BingApiUrl"] = "https://api.cognitive.microsoft.com/bing/v7.0/search";

            A.CallTo(() => _searcherFactory.CreateGoogleSearcher())
                .Returns(fakeGoogleSearcher);

            A.CallTo(() => _searcherFactory.CreateBingSearcher())
                .Returns(fakeBingSearcher);

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            Assert.IsInstanceOf<List<SearchResult>>(result);
            Assert.AreNotEqual(result.Count, notExpectedResultCount);
        }

        [Test]
        public void GetSearchResultsAsync_QueryNullOrEmpty_ReturnsNullWhenQueryNullOrEmpty()
        {
            //Arrange
            var query = "";
            var fakeGoogleSearcher = A.Fake<GoogleSearcher>();
            var fakeBingSearcher = A.Fake<BingSearcher>();
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => fakeGoogleSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);
            A.CallTo(() => fakeBingSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetSearchResultsAsync_NotValidFlags_ReturnsNull()
        {
            //Arrange
            var query = "Skateboard";
            var fakeGoogleSearcher = A.Fake<GoogleSearcher>();
            var fakeBingSearcher = A.Fake<BingSearcher>();
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => fakeGoogleSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);
            A.CallTo(() => fakeBingSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);
            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "null";

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetSearchResultsAsync_FlagsAreFalse_ReturnsNull()
        {
            //Arrange
            var query = "Skateboard";
            var fakeGoogleSearcher = A.Fake<GoogleSearcher>();
            var fakeBingSearcher = A.Fake<BingSearcher>();
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => fakeGoogleSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);
            A.CallTo(() => fakeBingSearcher.GetSearchResultsAsync(query))
                .Returns(listOfResults);


            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "false";
            ConfigurationManager.AppSettings["BingSearchEnabled"] = "false";
            ConfigurationManager.AppSettings["BingApiUrl"] = "https://api.cognitive.microsoft.com/bing/v7.0/search";

            A.CallTo(() => _searcherFactory.CreateGoogleSearcher())
                .Returns(fakeGoogleSearcher);

            A.CallTo(() => _searcherFactory.CreateBingSearcher())
                .Returns(fakeBingSearcher);

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetAllSearchQueries_SearchQueriesExists_ReturnsListOfSearchQueries()
        {
            //arrange
            var listOfResults = new List<SearchQuery>();
            listOfResults.Add(new SearchQuery
            {
                Id = 1111,
                QueryText = "Skateboard"
            });

            A.CallTo(() => _searchRepository.GetAllSearchQueries())
                .Returns(listOfResults);

            //Act
            IEnumerable<SearchQuery> result = _searchService.GetAllSearchQueries();

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchQuery>>(result);
        }

        [Test]
        public void GetSearchResultsByQueryId_QueryIdIsValid_ReturnsListOfSearchResults()
        {
            //Arrange
            int id = 222;
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => _searchRepository.GetSearchResultsByQueryId(id))
                .Returns(listOfResults);

            //Act
            IEnumerable<SearchResult> result = _searchService.GetSearchResultsByQueryId(id);

            //Assert
            Assert.IsInstanceOf<IEnumerable<SearchResult>>(result);
        }

        [Test]
        public void GetSearchResultsByQueryId_QueryIdLesserThanOne_ReturnsNull()
        {
            //Arrange
            int id = -1;
            var listOfResults = new List<SearchResult>();
            listOfResults.Add(new SearchResult
            {
                Id = 1111,
                Link = "www.skateboard.com",
                Title = "Best skateboards in the world"
            });

            A.CallTo(() => _searchRepository.GetSearchResultsByQueryId(id))
                .Returns(listOfResults);

            //Act
            IEnumerable<SearchResult> result = _searchService.GetSearchResultsByQueryId(id);

            //Assert
            Assert.IsNull(result);
        }
    }
}
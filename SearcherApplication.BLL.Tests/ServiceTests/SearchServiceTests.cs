﻿using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SearcherApplication.BLL.Interfaces;
using SearcherApplication.BLL.Services;
using SearcherApplication.DAL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;
using System;
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

        [Test]
        public void SearchService_GetSearchResultsAsync_ReturnsListOfSearchResults()
        {
            //Arrange
            var query = "Skateboard";
            int notExpectedResultCount = 0;
            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "true";
            ConfigurationManager.AppSettings["BingSearchEnabled"] = "true";
            ConfigurationManager.AppSettings["BingApiUrl"] = "https://api.cognitive.microsoft.com/bing/v7.0/search";

            A.CallTo(() => _searcherFactory.CreateGoogleSearcher())
                .Returns(new GoogleSearcher("AIzaSyCO4kYnDH22vhXPw4VQBjxczUT7hp1egAo", "005622240092378482429:d5rlwol90fg"));

            A.CallTo(() => _searcherFactory.CreateBingSearcher())
                .Returns(new BingSearcher("a21e4c95836141d7b75f311c6ba9fe21"));

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            NUnit.Framework.Assert.IsInstanceOf<List<SearchResult>>(result);
            NUnit.Framework.Assert.AreNotSame(result.Count, notExpectedResultCount);
        }

        [Test]
        public void SearchService_GetSearchResultsAsync_ReturnsNullWhenQueryNullOrEmpty()
        {
            //Arrange
            var query = "";

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            NUnit.Framework.Assert.IsNull(result);
        }

        [Test]
        public void SearchService_GetSearchResultsAsync_ReturnNullWhenFlagsAreNotValid()
        {
            //Arrange
            var query = "Skateboard";
            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "null";

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            NUnit.Framework.Assert.IsNull(result);
        }

        [Test]
        public void SearchService_GetSearchResultsAsync_ReturnsNullWhenListOfTasksIsEmpty()
        {
            //Arrange
            var query = "Skateboard";
            ConfigurationManager.AppSettings["GoogleSearchEnabled"] = "false";
            ConfigurationManager.AppSettings["BingSearchEnabled"] = "false";
            ConfigurationManager.AppSettings["BingApiUrl"] = "https://api.cognitive.microsoft.com/bing/v7.0/search";

            A.CallTo(() => _searcherFactory.CreateGoogleSearcher())
                .Returns(new GoogleSearcher("AIzaSyCO4kYnDH22vhXPw4VQBjxczUT7hp1egAo", "005622240092378482429:d5rlwol90fg"));

            A.CallTo(() => _searcherFactory.CreateBingSearcher())
                .Returns(new BingSearcher("a21e4c95836141d7b75f311c6ba9fe21"));

            //Act
            List<SearchResult> result = _searchService.GetSearchResultsAsync(query).Result;

            //Assert
            NUnit.Framework.Assert.IsNull(result);
        }

        [Test]
        public void SearchService_GetAllSearchQueries_ReturnsListOfSearchQueries()
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
            NUnit.Framework.Assert.IsInstanceOf<IEnumerable<SearchQuery>>(result);
        }

        [Test]
        public void SearchService_GetSearchResultsByQueryId_ReturnsListOfSearchResults()
        {
            //Arrange
            int id = 4;
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
            NUnit.Framework.Assert.IsInstanceOf<IEnumerable<SearchResult>>(result);
        }

        [Test]
        public void SearchService_GetSearchResultsByQueryId_ReturnsNullWhenIdIsLesserThanOne()
        {
            //Arrange
            int id = -1;

            //Act
            IEnumerable<SearchResult> result = _searchService.GetSearchResultsByQueryId(id);

            //Assert
            NUnit.Framework.Assert.IsNull(result);
        }
    }
}
using SearcherApplication.BLL.Interfaces;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SearcherApplication.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetSearchResults(string query)
        {
            List<SearchResult> results = await _searchService.GetSearchResultsAsync(query);
            if (results == null)
            {
                ViewBag.SearchErrorMessage = $"Search by query \"{query}\" yielded no results.";
                return View("EmptySearch");
            }
            ViewBag.SearchMessage = $"On request \"{query}\" the following results were obtained:";
            return View(results);
        }

        [HttpGet]
        public ActionResult GetSearchHistory(string query)
        {
            List<SearchQuery> results = _searchService.GetSearchQueries();
            if (results.Count == 0)
            {
                ViewBag.SearchErrorMessage = "There are no search queries in the system.";
                return View("EmptySearch");
            }

            return View(results);
        }

        [HttpGet]
        public ActionResult ViewQueryResults(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.SearchErrorMessage = "Incorrect id of the query.";
                return View("EmptySearch");
            }

            List<SearchResult> results = _searchService.GetSearchResultsByQuery(id.Value);
            if (results.Count == 0)
            {
                ViewBag.SearchErrorMessage = "There are no results found for this query.";
                return View("EmptySearch");
            }

            return View("GetSearchResults", results);
        }
    }
}
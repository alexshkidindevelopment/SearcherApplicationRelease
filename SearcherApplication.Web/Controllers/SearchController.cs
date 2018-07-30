using SearcherApplication.BLL.Interfaces;
using SearcherApplication.Models.DataModels;
using SearcherApplication.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
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
            if (results?.Count == 0)
            {
                return View("EmptySearch", new EmptySearchViewModel($"Search by query \"{query}\" yielded no results."));
            }

            //TODO: Create view model for this case
            ViewBag.SearchMessage = $"On request \"{query}\" the following results were obtained:";

            return View("GetSearchResults", results);
        }

        [HttpGet]
        public ActionResult GetSearchHistory(string query)
        {
            IEnumerable<SearchQuery> results = _searchService.GetAllSearchQueries();
            if (results?.Count() == 0)
            {
                return View("EmptySearch", new EmptySearchViewModel("There are no search queries in the system."));
            }

            return View(results);
        }

        [HttpGet]
        public ActionResult ViewQueryResults(int? id)
        {
            if (!id.HasValue)
            {
                return View("EmptySearch", new EmptySearchViewModel("Incorrect id of the query."));
            }

            IEnumerable<SearchResult> results = _searchService.GetSearchResultsByQueryId(id.Value);
            if (results?.Count() == 0)
            {
                return View("EmptySearch", new EmptySearchViewModel("There are no results found for this query."));
            }

            return View("GetSearchResults", results);
        }
    }
}
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
            List<SearchResult> results = await _searchService.GetSearchResults(query);
            if (results == null)
            {
                ViewBag.SearchErrorMessage = $"Search by query \"{query}\" yielded no results";
                return View("EmptySearch");
            }
            ViewBag.SearchMessage = $"On request \"{query}\" the following results were obtained:";
            return View(results);
        }

        [HttpGet]
        public async Task<ActionResult> GetSearchHistory(string query)
        {
            return View();
        }
    }
}
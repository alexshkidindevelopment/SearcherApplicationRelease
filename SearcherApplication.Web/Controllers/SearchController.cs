using SearcherApplication.BLL.Interfaces;
using SearcherApplication.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SearcherApplication.Web.Controllers
{
    public class SearchController : Controller
    {
        private ISearchService _searchService;

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
        public ActionResult GetSearchResults(string query)
        {
            Task<List<SearchResult>> results = _searchService.GetSearchResults(query);
            return View(results);
        }
    }
}
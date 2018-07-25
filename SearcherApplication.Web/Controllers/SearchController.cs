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
            return View(await _searchService.GetSearchResults(query));
        }
    }
}
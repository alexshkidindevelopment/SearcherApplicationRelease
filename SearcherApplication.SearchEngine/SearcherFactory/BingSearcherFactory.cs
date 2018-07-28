using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;

namespace SearcherApplication.SearchEngine.SearcherFactory
{
    public class BingSearcherFactory : AbstractSearcherFactory
    {
        public override ISearcher CreateSearcher()
        {
            return new BingSearcher(bingSearchSystem.ApiKey);
        }
    }
}
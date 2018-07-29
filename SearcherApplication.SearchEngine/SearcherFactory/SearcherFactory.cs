using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;

namespace SearcherApplication.SearchEngine.SearcherFactory
{
    public class SearcherFactory : AbstractSearcherFactory
    {
        public override ISearcher CreateGoogleSearcher()
        {
            return new GoogleSearcher(GoogleSearchSystem.ApiKey, GoogleSearchSystem.SearchEngineId);
        }

        public override ISearcher CreateBingSearcher()
        {
            return new BingSearcher(BingSearchSystem.ApiKey);
        }
    }
}
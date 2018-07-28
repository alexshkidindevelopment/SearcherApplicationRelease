using SearcherApplication.SearchEngine.Interfaces;
using SearcherApplication.SearchEngine.Searchers;

namespace SearcherApplication.SearchEngine.SearcherFactory
{
    public class GoogleSearcherFactory : AbstractSearcherFactory
    {
        public override ISearcher CreateSearcher()
        {
            return new GoogleSearcher(googleSearchSystem.ApiKey, googleSearchSystem.SearchEngineId);
        }
    }
}
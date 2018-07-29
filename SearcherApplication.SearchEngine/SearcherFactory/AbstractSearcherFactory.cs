using Newtonsoft.Json;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SearcherApplication.SearchEngine.SearcherFactory
{
    public abstract class AbstractSearcherFactory
    {
        protected static readonly SearchEngineSettings bingSearchSystem;

        protected static readonly SearchEngineSettings googleSearchSystem;

        //protected static readonly SearchEngineSettings yahooSearchSystem;

        static AbstractSearcherFactory()
        {
            string searchSystemsJson = ConfigurationManager.AppSettings["SearchSystems"];
            List<SearchEngineSettings> searchSystems = 
                JsonConvert.DeserializeObject<List<SearchEngineSettings>>(searchSystemsJson);

            googleSearchSystem = searchSystems.Where(s => s.Name == "Google").First();
            bingSearchSystem = searchSystems.Where(s => s.Name == "Bing").First();
            //yahooSearchSystem = searchSystems.Where(s => s.Name == "Yahoo").First();
        }

        public abstract ISearcher CreateBingSearcher();

        public abstract ISearcher CreateGoogleSearcher();
    }
}
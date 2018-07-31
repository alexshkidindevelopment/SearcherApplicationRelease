using Newtonsoft.Json;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace SearcherApplication.SearchEngine.SearcherFactory
{
    public abstract class AbstractSearcherFactory : ISearcherFactory
    {
        protected static SearchEngineSettings BingSearchSystem { get; }

        protected static SearchEngineSettings GoogleSearchSystem { get; }

        static AbstractSearcherFactory()
        {
            string projectDirectory = Directory.GetParent(HostingEnvironment.ApplicationPhysicalPath).Parent.FullName;
            string configPath = $"{projectDirectory}/SearcherApplication.SearchEngine/Configs/SearchEnginesConfigDev.json";
            List<SearchEngineSettings> searchSystems = 
                JsonConvert.DeserializeObject<List<SearchEngineSettings>>(File.ReadAllText(configPath));

            if (searchSystems == null)
            {
                throw new Exception("There are no search systems in the JSON");
            }

            GoogleSearchSystem = searchSystems.Where(s => s.Name == "Google").FirstOrDefault();
            BingSearchSystem = searchSystems.Where(s => s.Name == "Bing").FirstOrDefault();

            //TODO: Resolve this problem later
            if (GoogleSearchSystem == null && BingSearchSystem == null)
            {
                throw new Exception("There are no needed search systems in the config");
            }
        }

        public abstract ISearcher CreateBingSearcher();

        public abstract ISearcher CreateGoogleSearcher();
    }
}
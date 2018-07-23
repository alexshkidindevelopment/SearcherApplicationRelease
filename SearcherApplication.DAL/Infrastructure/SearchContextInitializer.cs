using SearcherApplication.Models.DataModels;
using System.Data.Entity;

namespace SearcherApplication.DAL.Infrastructure
{
    public class SearchContextInitializer : CreateDatabaseIfNotExists<SearchContext>
    {
        protected override void Seed(SearchContext db)
        {
            SearchSystem google = new SearchSystem
            {
                Name = "Google",
                ApiKey = "AIzaSyCO4kYnDH22vhXPw4VQBjxczUT7hp1egAo",
                SearchEngineId = "005622240092378482429:d5rlwol90fg"
            };
            SearchSystem bing = new SearchSystem
            {
                Name = "Bing",
                ApiKey = "a21e4c95836141d7b75f311c6ba9fe21"
            };
            SearchSystem yahoo = new SearchSystem
            {
                Name = "Yahoo",
                ApiKey = "unknown"
            };

            db.SearchSystems.Add(google);
            db.SearchSystems.Add(bing);
            db.SearchSystems.Add(yahoo);
            db.SaveChanges();
        }
    }
}
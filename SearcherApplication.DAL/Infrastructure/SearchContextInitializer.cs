using SearcherApplication.Models.DataModels;
using System.Data.Entity;

namespace SearcherApplication.DAL.Infrastructure
{
    public class SearchContextInitializer : CreateDatabaseIfNotExists<SearchHistoryStorageContext>
    {
        protected override void Seed(SearchHistoryStorageContext db)
        {
            //TODO: Add test data later
            //db.SaveChanges();
        }
    }
}
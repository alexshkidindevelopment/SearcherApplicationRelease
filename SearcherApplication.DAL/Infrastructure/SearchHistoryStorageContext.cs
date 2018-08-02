using SearcherApplication.Models.DataModels;
using System.Data.Entity;

namespace SearcherApplication.DAL.Infrastructure
{
    public class SearchHistoryStorageContext : DbContext
    {
        static SearchHistoryStorageContext()
        {
            Database.SetInitializer(new SearchContextInitializer());
        }

        public SearchHistoryStorageContext()
            : base("SearchHistoryStorage")
        { }

        public virtual DbSet<SearchResult> SearchResults { get; set; }

        public virtual DbSet<SearchQuery> SearchQueries { get; set; }
    }
}
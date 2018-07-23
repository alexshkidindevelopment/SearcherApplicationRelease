using SearcherApplication.Models.DataModels;
using System.Data.Entity;

namespace SearcherApplication.DAL.Infrastructure
{
    public class SearchContext : DbContext
    {
        static SearchContext()
        {
            Database.SetInitializer(new SearchContextInitializer());
        }

        public SearchContext()
            : base("DbConnection")
        { }

        public DbSet<SearchResult> SearchedResults { get; set; }

        public DbSet<SearchQuery> SearchQueries { get; set; }

        public DbSet<SearchSystem> SearchSystems { get; set; }
    }
}
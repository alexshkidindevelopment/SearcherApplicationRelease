using System.ComponentModel.DataAnnotations;

namespace SearcherApplication.Models.DataModels
{
    public class SearchResult
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }

        public int? SearchQueryId { get; set; }

        public virtual SearchQuery SearchQuery { get; set; }
    }
}
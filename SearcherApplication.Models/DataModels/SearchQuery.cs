using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherApplication.Models.DataModels
{
    public class SearchQuery
    {
        public SearchQuery()
        {
            SearchResults = new List<SearchResult>();
        }

        public int Id { get; set; }

        [Required]
        public string QueryText { get; set; }

        public virtual ICollection<SearchResult> SearchResults { get; set; }
    }
}
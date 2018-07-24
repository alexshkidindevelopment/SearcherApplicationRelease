using System.ComponentModel.DataAnnotations;

namespace SearcherApplication.Models.DataModels
{
    public class SearchEngineSettings
    {
        [Required]
        [MaxLength(50), MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250), MinLength(4)]
        public string ApiKey { get; set; }

        [MaxLength(250), MinLength(4)]
        public string SearchEngineId { get; set; }
    }
}
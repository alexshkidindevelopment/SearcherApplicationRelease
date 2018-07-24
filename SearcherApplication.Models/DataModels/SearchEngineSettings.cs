using System.ComponentModel.DataAnnotations;

namespace SearcherApplication.Models.DataModels
{
    public class SearchEngineSettings
    {
        public string Name { get; set; }
        
        public string ApiKey { get; set; }
        
        public string SearchEngineId { get; set; }
    }
}
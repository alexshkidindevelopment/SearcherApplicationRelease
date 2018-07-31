namespace SearcherApplication.Models.DataModels
{
    //TODO: change this later
    public class BingResultModel
    {
        public BingWebPages webPages { get; set; }
    }

    public class BingWebPages
    {
        public BingSearchResult[] value { get; set; }
    }

    public class BingSearchResult
    {
        public string url { get; set; }

        public string name { get; set; }
    }
}
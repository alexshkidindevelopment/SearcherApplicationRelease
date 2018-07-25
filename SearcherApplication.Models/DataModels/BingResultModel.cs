using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherApplication.Models.DataModels
{
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
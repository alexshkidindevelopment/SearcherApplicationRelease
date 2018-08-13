using Newtonsoft.Json;
using SearcherApplication.Models.DataModels;
using SearcherApplication.SearchEngine.EngineModels;
using SearcherApplication.SearchEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SearcherApplication.SearchEngine.Searchers
{
    public class BingSearcher : ISearcher
    {
        private readonly string _apiKey;
        private readonly string _bingUrl = ConfigurationManager.AppSettings["BingApiUrl"];
        private const int _countOfRecords = 10;

        public BingSearcher(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async virtual Task<List<SearchResult>> GetSearchResultsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            WebRequest wbReq = CreateRequest(query);

            //TODO: Need to add a logger, NLog for example
            try
            {
                using (var hResp = await wbReq.GetResponseAsync() as HttpWebResponse)
                {
                    string strJSON;
                    using (var reader = new StreamReader(hResp.GetResponseStream()))
                    {
                        strJSON = reader.ReadToEnd();
                    }

                    BingResultModel result = JsonConvert.DeserializeObject<BingResultModel>(strJSON);

                    return await Task.Run(() => { return Map(result); });
                }
            }
            catch (WebException ex) //That exception causes when API KEY was expired
            {
                await Task.Delay(10000);
                return null;
            }
            catch (Exception ex)
            {
                await Task.Delay(10000);
                return null;
            }
        }

        private List<SearchResult> Map(BingResultModel search)
        {
            if (search?.webPages == null)
            {
                return null;
            }

            List<SearchResult> results = new List<SearchResult>();
            foreach (BingSearchResult result in search.webPages.value)
            {
                SearchResult tempResult = new SearchResult
                {
                    Title = result.name,
                    Link = result.url
                };
                results.Add(tempResult);
            }

            return results;
        }

        private WebRequest CreateRequest(string query)
        {
            string uriQuery = $"{_bingUrl}?q={Uri.EscapeDataString(query)}&count={_countOfRecords}";
            WebRequest wbReq = WebRequest.Create(uriQuery);
            wbReq.Headers["Ocp-Apim-Subscription-Key"] = _apiKey;

            return wbReq;
        }
    }
}
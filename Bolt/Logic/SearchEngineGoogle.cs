using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class SearchEngineGoogle : SearchEngine
    {
        public SearchEngineGoogle(DbSearchContext context, IMemoryCache cache)
            : base(context, cache, "Google")
        {
        }

        protected override string GetSearchUrl(string searchWord)
        {
            return $"https://www.google.com/search?q={searchWord}";
        }
        public override async Task<IEnumerable<string>> GetTitles(string searchWord)
        {
            try
            {
                IEnumerable<string> titles;
                if (!_cache.TryGetValue($"{_searchEngineName}_{searchWord}", out titles))
                {
                    HttpClient httpClient = new HttpClient();
                    string url = GetSearchUrl(searchWord);
                    HtmlWeb htmlWeb = new HtmlWeb();
                    HtmlDocument htmlDocument = await htmlWeb.LoadFromWebAsync(url);
                    HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'r')]/h3");
                    if (results != null)
                    {
                        titles = results.Take(5).Select(a => a.InnerText);
                        await PersistResults(titles);
                        SetCashResults(titles, searchWord);
                        return titles;
                    }
                    return null;
                }
                return titles;
            }
            catch (HttpRequestException e)
            {
                //TODO: logger
                return null;
            }
        }

    }
}

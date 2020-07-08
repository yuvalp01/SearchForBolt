using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class SearchEngineBing:SearchEngine
    {
        public SearchEngineBing(DbSearchContext context,IMemoryCache cache)
            :base(context, cache, "Bing")
        {
          
        }

        protected override string GetSearchUrl(string searchWord)
        {
            return $"https://www.bing.com/search?q={searchWord}";
        }

        public override async Task<IEnumerable<string>> GetTitles(string searchWord)
        {
            try
            {
                IEnumerable<string> titles;
                if (!_cache.TryGetValue($"{ _searchEngineName}_{searchWord}", out titles))
                {
                    HttpClient httpClient = new HttpClient();
                    string url = GetSearchUrl(searchWord);
                    HtmlWeb htmlWeb = new HtmlWeb();
                    HtmlDocument htmlDocument = await htmlWeb.LoadFromWebAsync(url);
                    HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes("//ol[@id='b_results']/li/h2");
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

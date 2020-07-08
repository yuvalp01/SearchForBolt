using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class SearchEngineFactory
    {
        private SearchEngine searchEngine;
        public SearchEngineFactory(string searchEngineName, DbSearchContext context, IMemoryCache cache)
        {
            
            switch (searchEngineName.ToLower())
            {
                case "google":
                    searchEngine = new SearchEngineGoogle(context, cache);
                    break;
                case "bing":
                    searchEngine = new SearchEngineBing(context, cache);
                    break;
                default:
                    throw new ArgumentException("Unknown search engine");
            }
        }
        public SearchEngine GetSearchEngine()
        {
            return searchEngine;
        }
    }
}

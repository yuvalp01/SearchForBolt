using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class SearchRequestsFactory
    {
        private ISearchRequest searchRequest;
        public SearchRequestsFactory(string searchWord, string searchEngineName)
        {
            
            switch (searchEngineName.ToLower())
            {
                case "google":
                    searchRequest = new Google(searchEngineName, searchWord);
                    break;
                case "bing":
                    searchRequest = new Bing(searchEngineName, searchWord);
                    break;
                default:
                    throw new ArgumentException("Unknown search engine");
            }
        }
        public ISearchRequest GetSearchEngine()
        {
            return searchRequest;
        }
    }
}

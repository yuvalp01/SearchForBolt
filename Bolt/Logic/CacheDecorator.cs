using Bolt.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{


    public class CacheDecorator : ISearchRequestManager
    {
        private readonly ISearchRequestManager _requestManager;
        private readonly IMemoryCache _cache;
        private const double CACHE_DAYS = 1;
        public ISearchRequest SearchRequest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CacheDecorator(ISearchRequestManager requestManager, IMemoryCache cache)
        {
            _requestManager = requestManager;
            _cache = cache;
        }

        public async Task<IEnumerable<string>> GetTitles()
        {
            IEnumerable<string> titles;
            string key = GetKey(_requestManager.SearchRequest);
            if (!_cache.TryGetValue(key, out titles))
            {
                titles = await _requestManager.GetTitles();
                _cache.Set(key, titles, TimeSpan.FromDays(CACHE_DAYS));
            }
            return  titles;

        }

        private string GetKey(ISearchRequest searchRequest)
        {
            return $"{searchRequest.EngineName}_{searchRequest.SearchWord}";
        }
    }
}

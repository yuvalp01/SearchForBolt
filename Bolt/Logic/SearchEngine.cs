using Bolt.Models;
using Bolt.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public abstract class SearchEngine
    {
        const double CACHE_DAYS = 1;
        private DbSearchContext _context;
        protected string _searchEngineName;
        protected IMemoryCache _cache;
        public SearchEngine(DbSearchContext context, IMemoryCache cache, string searchEngineName)
        {
            _context = context;
            _cache = cache;
            _searchEngineName = searchEngineName;
        }

        public abstract Task<IEnumerable<string>> GetTitles(string searchWord);
        protected abstract string GetSearchUrl(string searchWord);
        protected async Task PersistResults(IEnumerable<string> titles)
        {
            SearchResultsRepository repository = new SearchResultsRepository(_context);
            List<SearchResult> results = new List<SearchResult>();
            foreach (var title in titles)
            {
                results.Add(new SearchResult
                {
                    Title = title,
                    EnteredDate = DateTime.Today,
                    SearchEngine = _searchEngineName
                });
            }
            await repository.AddSearchResults(results);


        }
        protected void SetCashResults(IEnumerable<string> titles, string searchWord)
        {
            _cache.Set($"{_searchEngineName}_{searchWord}", titles, TimeSpan.FromDays(CACHE_DAYS));
        }

    }
}

using Bolt.Models;
using Bolt.Repositories;
using HtmlAgilityPack;
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
    public class SearchEngineNew : ISearchRequestManager
    {
        private DbSearchContext _context;
        protected string _searchEngineName;

        public ISearchRequest SearchEngine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SearchWord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SearchEngineNew(DbSearchContext context, IMemoryCache cache, string searchEngineName)
        {
            _context = context;
            _searchEngineName = searchEngineName;
        }

        public async Task<IEnumerable<string>> GetTitles(string searchWord)
        {
            try
            {
                IEnumerable<string> titles;
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
            catch (HttpRequestException e)
            {
                //TODO: logger
                return null;
            }
        }
        protected override string GetSearchUrl(string searchWord)
        {
            return $"https://www.google.com/search?q={searchWord}";
        }
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

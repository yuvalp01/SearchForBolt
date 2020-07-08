using Bolt.Models;
using Bolt.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class SearchRequestManager : ISearchRequestManager
    {

        public ISearchRequest SearchRequest { get; set; }
        private DbSearchContext _context;
        public SearchRequestManager(ISearchRequest searchRequest, DbSearchContext context)
        {
            SearchRequest = searchRequest;
            _context = context;
        }
        public async Task<IEnumerable<string>> GetTitles()
        {
            try
            {
                IEnumerable<string> titles;
                HttpClient httpClient = new HttpClient();
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = await htmlWeb.LoadFromWebAsync(SearchRequest.Url);
                HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes(SearchRequest.XpathPatern);
                if (results != null)
                {
                    titles = results.Take(5).Select(a => a.InnerText);
                    await PersistResults(titles);
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
        private async Task PersistResults(IEnumerable<string> titles)
        {
            SearchResultsRepository repository = new SearchResultsRepository(_context);
            List<SearchResult> results = new List<SearchResult>();
            foreach (var title in titles)
            {
                results.Add(new SearchResult
                {
                    Title = title,
                    EnteredDate = DateTime.Today,
                    SearchEngine = SearchRequest.EngineName
                }); ;
            }
            await repository.AddSearchResults(results);


        }
    }
}

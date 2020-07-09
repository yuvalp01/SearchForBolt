using Bolt.Models;
using Bolt.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Get the top 5 seach results from the selected search engine
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetTitles()
        {
            try
            {
               
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = await htmlWeb.LoadFromWebAsync(SearchRequest.Url);
                HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes(SearchRequest.XpathPatern);
                if (results != null)
                {
                    IEnumerable<string> titles = results.Take(5).Select(a => a.InnerText);
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
        /// <summary>
        /// Persist the results to the db with the current date
        /// </summary>
        /// <param name="titles"></param>
        /// <returns></returns>
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

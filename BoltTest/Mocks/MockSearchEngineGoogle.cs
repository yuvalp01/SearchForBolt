using Bolt;
using Bolt.Logic;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTestBoltSearch
{
    public class MockSearchEngineGoogle : SearchEngine
    {
        public MockSearchEngineGoogle(DbSearchContext context)
    : base(context, "Google")
        {
        }
        public override async Task<IEnumerable<string>> GetTitles(string searchWord)
        {


            try
            {


                //using (var reader = File.OpenText("Mocks//GoogleMockResults.html"))
                //{
                //    var fileText = await reader.ReadToEndAsync();
                //    // Do something with fileText...
                //}
                string path = "Mocks//GoogleMockResults.html";
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.Load(path);
                HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'r')]/h3");
                if (results != null)
                {
                    var titles = results.Take(5).Select(a => a.InnerText);
                    await PersistResults(titles);
                    SetCashResults(titles, searchWord);
                    return titles;
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: logger
                return null;
            }



        }

        protected override string GetSearchUrl(string searchWord)
        {
            throw new NotImplementedException();
        }
    }
}

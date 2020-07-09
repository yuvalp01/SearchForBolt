using Bolt;
using Bolt.Logic;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTestBolt
{

    [TestClass]
    public class SearchEngineTest
    {

        private static DbSearchContext _dbContext;
        private static TestContext _testContext;

        [ClassInitialize()]
        public static void InitTestSuite(TestContext testContext)
        {
            _testContext = testContext;
            _dbContext = new InMemoryDbContextFactory().GetMockSearchDbContext();


        }

        [TestMethod]
        public void TestPersistResults()
        {
            ISearchRequest searchRequest = new SearchRequestsFactory("Insurance", "Google").GetSearchEngine();
            ISearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _dbContext);
            searchRequestManager.pe

        }

        [TestMethod]
        public void TestGoogleResults()
        {
            //ISearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _dbContext);
            //IEnumerable<string> titles = await searchRequestManager.GetTitles();

            HtmlDocument htmlDocument1 = new HtmlDocument();
            htmlDocument1.Load("Mocks/GoogleMockResults.html");
            string html = File.ReadAllText("Mocks/GoogleMockResults.html");
            HtmlDocument htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(html);

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument3 =  htmlWeb.LoadFromWebAsync("https://www.google.com/search?q=Insurance").Result;


            HtmlNodeCollection results1 = htmlDocument1.DocumentNode.SelectNodes("//div[@class='r']//h3");

            var xxx = results1.Select(a => a.InnerText);
            HtmlNodeCollection results00 = htmlDocument1.DocumentNode.SelectNodes("//div[@class='r']/h3");

            HtmlNodeCollection results2 = htmlDocument2.DocumentNode.SelectNodes(searchRequest.XpathPatern);
            HtmlNodeCollection results3 = htmlDocument3.DocumentNode.SelectNodes(searchRequest.XpathPatern);



        }

    }
}

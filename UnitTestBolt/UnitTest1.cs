using Bolt;
using Bolt.Logic;
using HtmlAgilityPack;
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

        //[ClassInitialize()]
        //public static void InitTestSuite(TestContext testContext)
        //{
        //    _testContext = testContext;
        //    _dbContext = new InMemoryDbContextFactory().GetMockSearchDbContext();


        //}

        [TestMethod]
        public void TestGoogleResults()
        {
            ISearchRequest searchRequest = new SearchRequestsFactory("Insurance", "Google").GetSearchEngine();
            //ISearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _dbContext);
            //IEnumerable<string> titles = await searchRequestManager.GetTitles();

            HtmlDocument htmlDocument = new HtmlDocument();
            string html = File.ReadAllText("Mocks/GoogleMockResults.html");
            // htmlDocument.LoadHtml(html);
            htmlDocument.Load("Mocks/GoogleMockResults.html");
            HtmlNodeCollection results = htmlDocument.DocumentNode.SelectNodes(searchRequest.XpathPatern);



        }

    }
}

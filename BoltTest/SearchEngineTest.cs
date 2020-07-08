using Bolt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestBoltSearch.Mocks;

namespace UnitTestBoltSearch
{
    [TestClass]
    public class SearchEngineTest
    {

        private static DbSearchContext _dbContext;
        private static TestContext _testContext;
        //private static IIssueRepository _issueRepository;

        [ClassInitialize()]
        public static void InitTestSuite(TestContext testContext)
        {
            _testContext = testContext;
            _dbContext = new InMemoryDbContextFactory().GetMockSearchDbContext();

        }

        [TestMethod]
        public void TestGoogleResults()
        {
            MockSearchEngineGoogle mockSearchEngineGoogle = new MockSearchEngineGoogle(_dbContext);
            var xxx = mockSearchEngineGoogle.GetTitles("bolt");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class Google: ISearchRequest
    {
        public string EngineName { get; set; }
        public string Url { get; set; }
        public string XpathPatern { get; set; }
        public string SearchWord { get; set; }
        
        public Google(string engineName, string searchWord)
        {
            SearchWord = searchWord;
            EngineName = engineName;
            Url = $"https://www.google.com/search?q={searchWord}";
            XpathPatern = "//div[contains(@class,'r')]/h3";
        }
    }
}

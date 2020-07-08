using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public class Bing: ISearchRequest
    {
        public string EngineName { get; set; }
        public string Url { get; set; }
        public string XpathPatern { get; set; }
        public string SearchWord { get; set; }
        


        public Bing(string engineName, string searchWord)
        {
            SearchWord = searchWord;
            EngineName = engineName;
            Url = $"https://www.bing.com/search?q={searchWord}";
            XpathPatern = "//ol[@id='b_results']/li/h2";

        }
    }
}

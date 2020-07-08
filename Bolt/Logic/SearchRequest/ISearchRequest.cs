using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public interface ISearchRequest
    {
        string SearchWord { get; set; }
        string EngineName { get; set; }
        string Url { get; set; }
        string XpathPatern { get; set; }
    }


}

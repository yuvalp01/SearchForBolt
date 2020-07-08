using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic
{
    public interface ISearchRequestManager
    {
        Task< IEnumerable<string>> GetTitles();
        ISearchRequest SearchRequest { get; set; }
        
    }
}

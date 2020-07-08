using Bolt.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Repositories
{
    public class SearchResultsRepository
    {
        private DbSearchContext _context;
        public SearchResultsRepository(DbSearchContext context)
        {
            _context = context;
        }

        public async Task AddSearchResults(IEnumerable<SearchResult> titles)
        {
            _context.SearchResults.AddRange(titles);
            await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private DbSearchContext _context;
        private IMemoryCache _cache;
        private IConfiguration _config;
        public SearchController(DbSearchContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("{searchWord}/{engine}")]
        public async Task<IActionResult> Get(string searchWord, string engine)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ISearchRequest searchRequest = new SearchRequestsFactory(searchWord, engine).GetSearchEngine();
            ISearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _context);
            CacheDecorator cacheDecorator = new CacheDecorator(searchRequestManager, _cache);
            IEnumerable<string> titles = await cacheDecorator.GetTitles();
            return Ok(titles);
        }
    }
}



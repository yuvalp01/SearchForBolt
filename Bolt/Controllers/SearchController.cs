using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private DbSearchContext _context;
        private IMemoryCache _cache;
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
            SearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _context);
            CacheDecorator cacheDecorator = new CacheDecorator(searchRequestManager, _cache);
            IEnumerable<string> titles = await cacheDecorator.GetTitles();
            return Ok(titles);
        }
    }
}
//if (!_cache.TryGetValue($"{searchWord}_{engine}", out titles))
//{
//    SearchEngine searchEngine = new SearchEngineFactory_old(engine, _context, _cache).GetSearchEngine();

//    results = await searchEngine.GetTitles(searchWord);
//    if (results == null)
//    {
//        return NotFound();
//    }
//}


using System.Collections.Generic;
using System.Threading.Tasks;
using Bolt.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;



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
            ISearchRequestManager searchRequestManager = new SearchRequestManager(searchRequest, _context);
            CacheDecorator cacheDecorator = new CacheDecorator(searchRequestManager, _cache);
            IEnumerable<string> titles = await cacheDecorator.GetTitles();
            return Ok(titles);
        }
    }
}



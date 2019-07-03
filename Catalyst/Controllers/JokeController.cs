using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Configuration;
using Catalyst.Models;
using Catalyst.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Catalyst.Controllers
{
    public class JokeController : Controller
    {
        private IMemoryCache _cache;
        private IJokeData _data;
        public JokeController(IJokeData data, IMemoryCache cache)
        {
            _cache = cache;
            _data = data;
        }

        // GET: Joke
        public async Task<ActionResult> Index()
        {
            IEnumerable<Joke> jokes;
            // Look for cache key.
            if (!_cache.TryGetValue(CacheKeys.Jokes, out jokes))
            {
                // Key not in cache, so get data.
                jokes = await _data.GetAll();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                // Save data in cache.
                _cache.Set(CacheKeys.Jokes, jokes, cacheEntryOptions);
            }

            return View(jokes.OrderByDescending(j => j.Id).ToList());
        }
    }
}
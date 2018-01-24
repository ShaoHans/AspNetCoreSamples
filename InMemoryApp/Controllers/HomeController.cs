using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryApp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string timeCacheKey = "Time_Cache";
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            if(!_memoryCache.TryGetValue(timeCacheKey, out DateTime cacheTime))
            {
                cacheTime = DateTime.Now;
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3));
                _memoryCache.Set(timeCacheKey, cacheTime, options);
            }
            ViewBag.CacheTime = cacheTime;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

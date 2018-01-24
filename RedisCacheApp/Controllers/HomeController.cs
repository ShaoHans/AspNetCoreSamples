using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisCacheApp.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace RedisCacheApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private string TimeCacheKey = "Time_Cache";
        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            var cacheTime = _cache.Get(TimeCacheKey);
            if(cacheTime == null)
            {
                cacheTime = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                _cache.Set(TimeCacheKey, cacheTime);
            }
            ViewBag.CacheTime = Encoding.UTF8.GetString(cacheTime);
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

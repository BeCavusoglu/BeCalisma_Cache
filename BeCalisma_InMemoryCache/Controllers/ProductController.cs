using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BeCalisma_InMemoryCache.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            // key de�erinin memory de var m� kontrol ediyor
        
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1); 

            options.SlidingExpiration = TimeSpan.FromSeconds(10);
                    
                _memoryCache.Set<string>("time", DateTime.Now.ToString(),options);

            return View();
        }

        public IActionResult Show()
        {
            /*
            _memoryCache.Remove("time");

            // �nce �eker yoksa olu�turur
            _memoryCache.GetOrCreate<string>("time", x =>
            {               
               return DateTime.Now.ToString();
            });


           ViewBag.zaman=_memoryCache.Get<string>("time");
            */

            _memoryCache.TryGetValue("time", out string timeCache);
           
            ViewBag.time = timeCache;
                    

            return View();
        }
       
    }
}

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
            // key deðerinin memory de var mý kontrol ediyor
            //1.Yol
            if (string.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }
            //2.Yol 
            if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            {
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.Remove("zaman");

            // Önce çeker yoksa oluþturur
            _memoryCache.GetOrCreate<string>("zaman", x =>
            {               
               return DateTime.Now.ToString();
            });


           ViewBag.zaman=_memoryCache.Get<string>("zaman");

            return View();
        }
       
    }
}

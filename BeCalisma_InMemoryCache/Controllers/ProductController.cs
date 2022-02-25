using BeCalisma_InMemoryCache.Models;
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
               
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            #region Option Time  
            /*Option Time             
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1); 
            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            */
            #endregion

            #region Priority
            /*
            options.Priority = CacheItemPriority.High;   en son siler
            options.Priority = CacheItemPriority.Low;    ilk silincek 
            options.Priority = CacheItemPriority.NeverRemove;  asla silinmeyecek        
            */
            #endregion

            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            options.Priority = CacheItemPriority.High;

            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value}=> sebep:{reason}");
            });

            _memoryCache.Set<string>("time", DateTime.Now.ToString(),options);

            Product product = new Product { 
            Id=1,
            Name="Ahmet",
            Price=2
            };

            _memoryCache.Set<Product>("product", product, options);

            return View();
        }

        public IActionResult Show()
        {
            /*
            Önce çeker yoksa oluþturur
            _memoryCache.GetOrCreate<string>("time", x =>
            {               
               return DateTime.Now.ToString();
            });

            _memoryCache.Remove("time");
           ViewBag.zaman=_memoryCache.Get<string>("time");
            */

            _memoryCache.TryGetValue("time", out string timeCache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.time = timeCache;
            ViewBag.callback = callback;
            ViewBag.product = _memoryCache.Get<Product>("product");
                    

            return View();
        }
       
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
           _memoryCache= memoryCache;
        }


        public IActionResult Index()
        {
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            _memoryCache.Set<string>("dateInMem", DateTime.Now.ToString(),cacheOptions);
            return View();
        }

        public IActionResult Show()
        {
            ViewBag.dateInMem = _memoryCache.Get<string>("dateInMem");
            return View();
        }

    }
}

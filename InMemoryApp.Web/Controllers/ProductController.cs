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

            cacheOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(10); // Mutlak zaman değeri ile cache de kalma süresi belirtiliyor
            cacheOptions.SlidingExpiration = TimeSpan.FromSeconds(2); // her istek geldiğinde ccach süresini 2 sn uzatıyor fakat absolute süresi varsa ve dolmuşsa yine siler

            cacheOptions.Priority = CacheItemPriority.Normal; // Cache in önem derecesini belirtir ve ona göre hangi öncelik varsa o sıraya göre siler neverRemove hiç silmez fakat chach dolarsa exep fırlatır

            
            _memoryCache.Set<string>("dateInMem", DateTime.Now.ToString(),cacheOptions); // Cache oluşturuluyor


            return View();
        }

        public IActionResult Show()
        {
            ViewBag.dateInMem = _memoryCache.Get<string>("dateInMem");// memory den belirtlen key e bağlı değer get ediliyor
            return View();
        }

    }
}

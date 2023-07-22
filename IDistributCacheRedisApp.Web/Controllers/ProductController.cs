using IDistributCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace IDistributCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;            
        }
        public async Task<IActionResult> Index()
        {
            //Burada cache de tutacağımız verinin optionslarını yapıyoruz, süre vs..
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();

            //SetString ile value key ve options ile set ediyoruz 
            cacheOptions.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(5);
            //Cache de değerin olup olmadığı yoksa oluşturmayı sağlıyoruz
            var value = _distributedCache.GetString("name2");
            if (value == null)
            {
                _distributedCache.SetString("name2", "betül", cacheOptions);
            }

            //Complex Typler için cache
            //Ürün oluşturduk
            Product product = new Product
            {
                Id= 1,
                Name ="Ürün-1",
                Price= 1000,
            };
            // Product class ı serialize ediyoruz
            string jsonProduct = JsonConvert.SerializeObject(product);
            //Burda da class ı cache yazıyoruz asenkron yazıyoruz
            await _distributedCache.SetStringAsync("product:1",jsonProduct,cacheOptions);


            return View();
        }

        public IActionResult Show()
        {
            //Değeri çağırıp viewbag ile gönderiyoruz
            ViewBag.cacheValue = _distributedCache.GetString("name2");

            //product:1 keyli data çekiliyor
            var getData = _distributedCache.GetString("product:1").ToString();

            //Deserialize edilerek product nesnesi oluşuyor
            Product p = JsonConvert.DeserializeObject<Product>(getData);
           
            return View(p);
        }

        public IActionResult Delete()
        {
            //Belirtilen cache i bellekten siliyoruz
            _distributedCache.Remove("name2");
            return View();
        }
    }
}

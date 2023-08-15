using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributCacheRedisApp.Web.Controllers
{
    public class FileController : Controller
    {

        private IDistributedCache _distributedCache;
        public FileController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImageCache()
        {
            //Dosyaya ulaşıyoruz
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/i10images.jpg");
            if (path != null)
            {
                // ulaştığımız dosyayı byt diziye çeviriyoruz
                byte[] imagesByte = System.IO.File.ReadAllBytes(path);
                // cache set ediyoruz
                _distributedCache.Set("images", imagesByte);
            }
            // cache atılan dosyayı get ediyoruz
            byte[] imageBytes = _distributedCache.Get("images");
            if (imageBytes != null) 
            {
                // ekranda göstermek için file dönüyoruz
                return File(imageBytes, "image/jpg");
            }

            return View();
        }
    }
}

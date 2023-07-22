using Microsoft.AspNetCore.Mvc;

namespace IDistributCacheRedisApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

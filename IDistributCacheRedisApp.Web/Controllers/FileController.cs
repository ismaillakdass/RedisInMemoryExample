using Microsoft.AspNetCore.Mvc;

namespace IDistributCacheRedisApp.Web.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

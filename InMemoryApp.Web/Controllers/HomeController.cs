using Microsoft.AspNetCore.Mvc;

namespace InMemoryApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

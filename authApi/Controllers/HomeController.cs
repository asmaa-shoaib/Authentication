using Microsoft.AspNetCore.Mvc;

namespace authApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

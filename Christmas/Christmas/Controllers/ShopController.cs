using Microsoft.AspNetCore.Mvc;

namespace Christmas.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

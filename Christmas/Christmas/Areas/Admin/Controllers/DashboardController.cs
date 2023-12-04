using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Areas.Admin.Controllers
{
    public class DashboardController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

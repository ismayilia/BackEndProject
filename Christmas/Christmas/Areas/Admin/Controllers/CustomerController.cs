using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _customerService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            await _customerService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));

        }
    }
}

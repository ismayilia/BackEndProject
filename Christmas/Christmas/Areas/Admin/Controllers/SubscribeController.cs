using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SubscribeController : Controller
	{
		private readonly ISubscribeServie _subscribeService;


		public SubscribeController(ISubscribeServie subscribeService)
		{
			_subscribeService = subscribeService;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _subscribeService.GetAllAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _subscribeService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}

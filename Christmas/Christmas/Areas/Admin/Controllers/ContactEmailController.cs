using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Contact;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ContactEmailController : Controller
	{
		private readonly IContactService _contactService;
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public ContactEmailController(IContactService contactService, AppDbContext context, IMapper mapper)
		{
			_contactService = contactService;
			_context = context;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _contactService.GetAllMessagesAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MessageDelete(int id)
		{
			await _contactService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> MessageDetail(int? id)
		{
			if (id is null) return BadRequest();

			ContactMessageVM contactMessage = await _contactService.GetMessageByIdAsync((int)id);

			if (contactMessage is null) return NotFound();

			return View(contactMessage);
		}
	}
}
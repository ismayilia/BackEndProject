using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Contact;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Controllers
{
    public class ContactController : Controller
	{
		private readonly AppDbContext _context;
		private readonly ISettingService _settingService;
		private readonly IMapper _mapper;
		private readonly IContactService _contactService;

		public ContactController(AppDbContext context, IMapper mapper, ISettingService settingService, IContactService contactService)
		{
			_context = context;
			_mapper = mapper;
			_settingService = settingService;
			_contactService = contactService;
		}
		public async Task<IActionResult> Index()
		{
			Dictionary<string, string> settingDatas = _settingService.GetSettings();
			ContactVM contact = await _contactService.GetData();

			ContactVM model = new()
			{
				Desc = contact.Desc,
				Phone = settingDatas["Phone"],
				Address = settingDatas["Address"],
				Email = settingDatas["Email"]
			};
			return View(model);
		}
	}
}

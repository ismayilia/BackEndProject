using AutoMapper;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.About;
using Christmas.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{

	public class ContactService : IContactService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;


		public ContactService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<ContactVM> GetData()
		{
			var info = await _context.ContactInfos.FirstOrDefaultAsync();

			return _mapper.Map<ContactVM>(info);
		}
	}
}

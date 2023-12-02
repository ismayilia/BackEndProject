using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Contact;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.About;
using Microsoft.EntityFrameworkCore;
using Christmas.Models;

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

		public async Task CreateAsync(ContactMessageCreateVM contact)
		{
			var data = _mapper.Map<ContactEmail>(contact);
			await _context.ContactEmails.AddAsync(data);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			ContactEmail dbContactEmail = await _context.ContactEmails.FirstOrDefaultAsync(m => m.Id == id);
			_context.ContactEmails.Remove(dbContactEmail);
			await _context.SaveChangesAsync();
		}

		public async Task<List<ContactMessageVM>> GetAllMessagesAsync()
		{
			return _mapper.Map<List<ContactMessageVM>>(await _context.ContactEmails.ToListAsync());
		}

		public async Task<ContactVM> GetData()
		{
			var info = await _context.ContactInfos.FirstOrDefaultAsync();

			return _mapper.Map<ContactVM>(info);
		}

		public async Task<ContactMessageVM> GetMessageByIdAsync(int id)
		{
			var datas = await _context.ContactEmails.FirstOrDefaultAsync(m => m.Id == id);
			ContactMessageVM contactMessage = _mapper.Map<ContactMessageVM>(datas);
			return contactMessage;
		}
	}
}

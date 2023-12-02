using Christmas.Areas.Admin.ViewModels.Contact;

namespace Christmas.Services.Interfaces
{
    public interface IContactService
	{
		Task<ContactVM> GetData();
		Task<List<ContactMessageVM>> GetAllMessagesAsync();
		Task CreateAsync(ContactMessageCreateVM contact);
		Task DeleteAsync(int id);
		Task<ContactMessageVM> GetMessageByIdAsync(int id);
	}
}

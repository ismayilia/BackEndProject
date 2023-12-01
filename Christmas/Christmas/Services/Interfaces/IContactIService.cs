using Christmas.Areas.Admin.ViewModels.Contact;

namespace Christmas.Services.Interfaces
{
    public interface IContactService
	{
		Task<ContactVM> GetData();
	}
}

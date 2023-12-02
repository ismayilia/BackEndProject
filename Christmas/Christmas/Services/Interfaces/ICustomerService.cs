using Christmas.Areas.Admin.ViewModels.Customer;

namespace Christmas.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllAsync();
        Task<CustomerVM> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}

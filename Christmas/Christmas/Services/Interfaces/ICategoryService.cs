using Christmas.Areas.Admin.ViewModels.Category;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<List<CategoryVM>> GetAllVMAsync();
        Task<CategoryVM> GetByIdWithoutTrackingAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(CategoryCreateVM category);
        Task EditAsync(CategoryEditVM category);
        Task<CategoryVM> GetByNameWithoutTrackingAsync(string name);
    }
}

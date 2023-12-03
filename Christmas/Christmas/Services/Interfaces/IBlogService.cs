using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Review;

namespace Christmas.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllWithTakeAsync();
        Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);
		Task DeleteAsync(int id);
	}
}

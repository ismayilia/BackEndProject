using Christmas.ViewModels.Blog;
using Christmas.ViewModels.Review;

namespace Christmas.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);
    }
}

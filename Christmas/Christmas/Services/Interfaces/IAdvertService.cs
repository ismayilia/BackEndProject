using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Slider;

namespace Christmas.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
        Task CreateAsync(AdvertCreateVM request);
        Task DeleteAsync(int id);
        Task<AdvertVM> GetByIdAsync(int id);
        Task EditAsync(AdvertEditVM request);
    }
}

using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;

namespace Christmas.Services.Interfaces
{
    public interface IAboutService
    {
        Task<AboutVM> GetDataAsync();
        Task<AboutVM> GetByIdAsync(int id);
		Task EditAsync(AboutEditVM request);
	}
}

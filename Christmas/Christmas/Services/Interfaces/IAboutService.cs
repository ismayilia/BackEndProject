using Christmas.ViewModels.About;
using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Blog;

namespace Christmas.Services.Interfaces
{
    public interface IAboutService
    {
        Task<AboutVM> GetDataAsync();
    }
}

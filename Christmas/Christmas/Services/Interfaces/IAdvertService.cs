using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Slider;

namespace Christmas.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
    }
}

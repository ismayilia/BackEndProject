using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM slider);
        Task DeleteAsync(int id);
        Task EditAsync(SliderEditVM slider);

    }
}

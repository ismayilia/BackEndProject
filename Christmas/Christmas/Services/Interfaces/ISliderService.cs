using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
    }
}

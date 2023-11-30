using Christmas.Models;
using Christmas.ViewModels.Slider;

namespace Christmas.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
    }
}

using Christmas.Areas.Admin.ViewModels.About;

namespace Christmas.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();

    }
}

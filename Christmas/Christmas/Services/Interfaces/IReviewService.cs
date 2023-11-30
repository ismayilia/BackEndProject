using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Review;

namespace Christmas.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
    }
}

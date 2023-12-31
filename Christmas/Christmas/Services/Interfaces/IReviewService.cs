﻿using Christmas.Areas.Admin.ViewModels.Review;
using Christmas.Areas.Admin.ViewModels.Advert;

namespace Christmas.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
        Task<ReviewVM> GetByIdWithIncludeAsync(int id);
        Task DeleteAsync(int id);
    }
}

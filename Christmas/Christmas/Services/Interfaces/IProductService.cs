using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
        Task<List<ProductVM>> ShowMoreOrLess(int take, int skip);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<Product> GetByIdAsync(int id);
        Task<List<ProductVM>> GetAllAsync();
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task DeleteAsync(int id);
    }
}

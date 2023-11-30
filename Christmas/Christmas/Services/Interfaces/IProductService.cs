using Christmas.ViewModels.Product;

namespace Christmas.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
        Task<List<ProductVM>> ShowMoreOrLess(int take, int skip);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
    }
}

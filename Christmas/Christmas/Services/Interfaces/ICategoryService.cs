using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}

using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(m=>m.Products).ToListAsync();
        }
    }
}

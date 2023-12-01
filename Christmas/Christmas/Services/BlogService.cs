using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.Advert;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        //private readonly IProductService _productService;
        //private readonly IBasketService _basketService;
        //private readonly ISliderService _sliderService;

        public BlogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BlogVM>> GetAllAsync()
        {
            var blogs = await _context.Blogs.Include(m=>m.Images).Include(m=> m.BlogTags).Take(3).ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }

        public async Task<BlogVM> GetByIdAsync(int id)
        {
            Blog blog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<BlogVM>(blog);
        }
    }
}

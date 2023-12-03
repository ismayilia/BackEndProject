using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.Advert;
using Microsoft.EntityFrameworkCore;
using Christmas.Helpers.Extentions;

namespace Christmas.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;


		public BlogService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
		{
			_context = context;
			_mapper = mapper;
			_env = env;
		}

		public async Task DeleteAsync(int id)
		{
			Blog dbBlog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


			_context.Blogs.Remove(dbBlog);
			await _context.SaveChangesAsync();


			foreach (var photo in dbBlog.Images)
			{

				string path = _env.GetFilePath("img/blog", photo.Image);

				if (File.Exists(path))
				{
					File.Delete(path);
				}


			}
		}

		public async Task<List<BlogVM>> GetAllAsync()
        {
            var blogs = await _context.Blogs.Include(m => m.Images).ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }

        public async Task<List<BlogVM>> GetAllWithTakeAsync()
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

using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Tag;
using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogDetailController(AppDbContext context, IBlogService blogService, IMapper mapper)
        {
            _context = context;
            
            _blogService = blogService;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index(int id)
        {
            BlogVM blog = await _blogService.GetByIdAsync(id);

            List<Tag> tags = await _context.Tags.ToListAsync();
            var tag = _mapper.Map<List<TagVM>>(tags);

            BlogDetailVM model =new BlogDetailVM()
            {
                Blog = blog,
                Tags = tag
            };
            return View(model);
        }
    }
}

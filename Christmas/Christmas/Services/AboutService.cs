using AutoMapper;
using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public AboutService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AboutVM> GetDataAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();

            return _mapper.Map<AboutVM>(about);
        }

        
    }
}

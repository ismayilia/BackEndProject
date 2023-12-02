using AutoMapper;
using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Microsoft.EntityFrameworkCore;
using Christmas.Helpers.Extentions;
using Christmas.Models;

namespace Christmas.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public AboutService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task EditAsync(AboutEditVM request)
		{
            string oldPath = _env.GetFilePath("img", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img", fileName);

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbAbout);

            dbAbout.Image = fileName;

            _context.Abouts.Update(dbAbout);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFile(newPath);
        }

		public async Task<AboutVM> GetByIdAsync(int id)
        {
            var datas = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            AboutVM about = _mapper.Map<AboutVM>(datas);
            return about;
        }

        public async Task<AboutVM> GetDataAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();

            return _mapper.Map<AboutVM>(about);
        }

        
    }
}

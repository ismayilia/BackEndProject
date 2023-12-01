using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Christmas.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdvertService(AppDbContext context,IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(AdvertCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string path = _env.GetFilePath("img/banners", fileName); //root change

            Advert entity = _mapper.Map<Advert>(request);

            entity.Image = fileName;

            await _context.Adverts.AddAsync(entity);
            await _context.SaveChangesAsync();


            await request.Photo.SaveFile(path);
        }

        public async Task DeleteAsync(int id)
        {
            Advert dbAdvert = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);


            _context.Adverts.Remove(dbAdvert);
            await _context.SaveChangesAsync();


            string path = _env.GetFilePath("img/banners", dbAdvert.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(AdvertEditVM request)
        {
            string oldPath = _env.GetFilePath("img/banners", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img/banners", fileName);

            Advert dbAdvert = await _context.Adverts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAdvert);

            dbAdvert.Image = fileName;

            _context.Adverts.Update(dbAdvert);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFile(newPath);
        }

        public async Task<List<AdvertVM>> GetAllAsync()
        {
            var adverts = await _context.Adverts.ToListAsync();

             return _mapper.Map<List<AdvertVM>>(adverts);
        }

        public async Task<AdvertVM> GetByIdAsync(int id)
        {
            var datas = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);
            AdvertVM advert = _mapper.Map<AdvertVM>(datas);
            return advert;
        }
    }
}

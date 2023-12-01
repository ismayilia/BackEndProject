using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public SliderService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(SliderCreateVM slider)
        {
            string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

            string path = _env.GetFilePath("img/hero", fileName);

            var data = _mapper.Map<Slider>(slider);

            data.Image = fileName;

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

            await slider.Photo.SaveFile(path);
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/hero/", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderEditVM slider)
        {


            string oldPath = _env.GetFilePath("img/hero/", slider.Image);

            string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

            string newPath = _env.GetFilePath("img/hero/", fileName);

            Slider dbSlider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == slider.Id);
            
            _mapper.Map(slider, dbSlider);

            dbSlider.Image = fileName;

            _context.Sliders.Update(dbSlider);
            await _context.SaveChangesAsync();


            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await slider.Photo.SaveFile(newPath);


        }

        public async Task<List<SliderVM>> GetAllAsync()
        {

            List<Slider> datas = await _context.Sliders.ToListAsync();

            return _mapper.Map<List<SliderVM>>(datas);

        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<SliderVM>(slider);
        }
    }
}

using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Setting;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public SettingService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        //public async Task DeleteAsync(int id)
        //{
        //    Setting setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

        //    _context.Settings.Remove(setting);
        //    await _context.SaveChangesAsync();

        //    if (setting.Value.Contains("png") || setting.Value.Contains("jpeg") || setting.Value.Contains("jpg"))
        //    {
        //        string path = _env.GetFilePath("img", setting.Value);

        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }
        //    }

        //}

        public async Task EditAsync(SettingEditVM setting)
        {
            if (setting.Value.Contains("jpg") || setting.Value.Contains("png") || setting.Value.Contains("jpeg"))
            {
                string oldPath = _env.GetFilePath("img", setting.Value);

                string fileName = $"{Guid.NewGuid()}-{setting.Image.FileName}";

                string newPath = _env.GetFilePath("img", fileName);

                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                dbSetting.Value = fileName;

                await _context.SaveChangesAsync();

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await setting.Image.SaveFile(newPath);
            }
            else
            {
                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                _mapper.Map(setting, dbSetting);

                _context.Settings.Update(dbSetting);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}

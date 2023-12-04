using Christmas.Areas.Admin.ViewModels.Setting;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
        Task<List<Setting>> GetAllAsync();

        //Task DeleteAsync(int id);
        Task<Setting> GetByIdAsync(int id);
        Task EditAsync(SettingEditVM setting);
    }
}

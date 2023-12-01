using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Areas.Admin.ViewModels.Advert;

namespace Christmas.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<TeamVM> GetByIdAsync(int id);
		Task DeleteAsync(int id);
        Task CreateAsync(TeamCreateVM team);
        Task EditAsync(TeamEditVM request);


    }
}

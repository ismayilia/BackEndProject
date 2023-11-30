using Christmas.ViewModels.About;

namespace Christmas.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();

    }
}

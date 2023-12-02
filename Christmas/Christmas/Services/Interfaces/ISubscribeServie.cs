using Christmas.Areas.Admin.ViewModels.Subscribe;

namespace Christmas.Services.Interfaces
{
	public interface ISubscribeServie
	{
		Task<List<SubscribeVM>> GetAllAsync();
		Task DeleteAsync(int id);
		Task CreateAsync(SubscribeCreateVM subscribe);
	}
}

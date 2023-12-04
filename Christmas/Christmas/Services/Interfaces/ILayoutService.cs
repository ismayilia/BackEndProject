using Christmas.Areas.Admin.ViewModels.Layout;

namespace Christmas.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<HeaderVM> GetHeaderDatas();
        FooterVM GetFooterDatas();
    }
}

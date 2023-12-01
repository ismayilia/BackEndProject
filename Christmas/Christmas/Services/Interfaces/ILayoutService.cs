using Christmas.Areas.Admin.ViewModels.Layout;

namespace Christmas.Services.Interfaces
{
    public interface ILayoutService
    {
        HeaderVM GetHeaderDatas();
        FooterVM GetFooterDatas();
    }
}

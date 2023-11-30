using Christmas.ViewModels.Layout;

namespace Christmas.Services.Interfaces
{
    public interface ILayoutService
    {
        HeaderVM GetHeaderDatas();
        FooterVM GetFooterDatas();
    }
}

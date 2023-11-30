using Christmas.Migrations;

namespace Christmas.ViewModels.About
{
    public class AboutPageVM
    {
        public AboutVM About { get; set; }
        public List<BrandVM> Brands { get; set; }
        public List<TeamVM> Teams { get; set; }
    }
}

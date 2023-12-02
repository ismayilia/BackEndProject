using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Areas.Admin.ViewModels.Review;
using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Areas.Admin.ViewModels.Subscribe;
using Christmas.Models;

namespace Christmas.Areas.Admin.ViewModels.Home
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<AdvertVM> Adverts { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public List<ProductVM> Products { get; set; }
		public SubscribeCreateVM Subscribe { get; set; }
	}
}

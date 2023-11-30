using Christmas.Models;
using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Blog;
using Christmas.ViewModels.Product;
using Christmas.ViewModels.Review;
using Christmas.ViewModels.Slider;

namespace Christmas.ViewModels.Home
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<AdvertVM> Adverts { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}

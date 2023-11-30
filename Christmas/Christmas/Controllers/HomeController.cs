using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Blog;
using Christmas.ViewModels.Home;
using Christmas.ViewModels.Review;
using Christmas.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IProductService _productService;
        private readonly IAdvertService _advertService;
        private readonly ISliderService _sliderService;
        private readonly IReviewService _reviewService;
        private readonly IBlogService _blogService;

        public HomeController(AppDbContext context, ISliderService sliderService,
                                                    IAdvertService advertService,
                                                    IReviewService reviewService,
                                                    IBlogService blogService)
        {
            _context = context;
            //_productService = productService;
            //_basketService = basketService;
            _sliderService = sliderService;
            _advertService = advertService;
            _reviewService = reviewService;
            _blogService = blogService;

        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<ReviewVM> reviews = await _reviewService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllAsync();


            HomeVM model = new()
            {
                Sliders = sliders,
                Adverts = adverts,
                Reviews = reviews,
                Blogs = blogs
            };


            return View(model);
        }
    }
}

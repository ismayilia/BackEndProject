using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Home;
using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Areas.Admin.ViewModels.Review;
using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Areas.Admin.ViewModels.Subscribe;
using Christmas.Data;
using Christmas.Services;
using Christmas.Services.Interfaces;
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
        private readonly IProductService _productService;
        private readonly ISubscribeServie _subscribeService;

        public HomeController(AppDbContext context, ISliderService sliderService,
                                                    IAdvertService advertService,
                                                    IReviewService reviewService,
                                                    IBlogService blogService,
                                                    IProductService productService,
                                                    ISubscribeServie subscribeService)
        {
            _context = context;
            //_productService = productService;
            //_basketService = basketService;
            _sliderService = sliderService;
            _advertService = advertService;
            _reviewService = reviewService;
            _blogService = blogService;
            _productService = productService;
            _productService = productService;
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<ReviewVM> reviews = await _reviewService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllAsync();
            List<ProductVM> products = await _productService.GetByTakeWithIncludes(3);


            HomeVM model = new()
            {
                Sliders = sliders,
                Adverts = adverts,
                Reviews = reviews,
                Blogs = blogs,
                Products = products
            };

            int productCount = await _productService.GetCountAsync();
            ViewBag.count = productCount;

            return View(model);
        }

        public async Task<IActionResult> LoadMore(int skipCount)
        {

            List<ProductVM> products = await _productService.ShowMoreOrLess(3, skipCount);

            return PartialView("_ProductsPartial", products);
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
		{

			await _subscribeService.CreateAsync(subscribe);
			return RedirectToAction("Index", "Home");
		}
	}
}

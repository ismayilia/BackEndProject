using AutoMapper;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        //private readonly IProductService _productService;
        //private readonly IBasketService _basketService;
        //private readonly ISliderService _sliderService;

        public ReviewService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReviewVM>> GetAllAsync()
        {
            var reviews = await _context.Reviews.Include(m=>m.Customer).ToListAsync();

            return _mapper.Map<List<ReviewVM>>(reviews);
        }
    }
}

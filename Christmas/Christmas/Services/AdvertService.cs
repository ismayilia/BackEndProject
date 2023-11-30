using AutoMapper;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.Advert;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        //private readonly IProductService _productService;
        //private readonly IBasketService _basketService;
        //private readonly ISliderService _sliderService;

        public AdvertService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            var adverts = await _context.Adverts.ToListAsync();

             return _mapper.Map<List<AdvertVM>>(adverts);
        }
    }
}

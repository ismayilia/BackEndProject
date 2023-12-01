using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        //private readonly IProductService _productService;
        //private readonly IBasketService _basketService;
        //private readonly ISliderService _sliderService;

        public SliderService(AppDbContext context/*, IProductService productService, IBasketService basketService, ISliderService sliderService*/)
        {
            _context = context;
            //_productService = productService;
            //_basketService = basketService;
            //_sliderService = sliderService;
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
          
                return await _context.Sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Image, Desc = m.Desc, Title = m.Title, Discount=m.Discount })
                                             .ToListAsync();
            
        }
    }
}

using Christmas.Areas.Admin.ViewModels.Cart;
using Christmas.Data;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Christmas.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;


        public CartController(AppDbContext context, 
                                                    ICartService cartService, 
                                                    IProductService productService)
        {
            _context = context;
            _cartService = cartService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            List<CartVM> carts = _cartService.GetDatasFromCookie();
            List<CartDetailVM> cartDetailVMs = new();

            foreach (var item in carts)
            {
                Product dbProduct = await _productService.GetByIdAsync(item.ProductId);

                cartDetailVMs.Add(new CartDetailVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(m => m.IsMain).Image,
                    Count = item.Count,
                    Total = dbProduct.Price * item.Count,
                });
            }
            return View(cartDetailVMs);
        }

		[HttpPost]
		public async Task<IActionResult> DeleteDataFromBasket(int? id)
		{
			var data = await _cartService.DeleteData(id);

			return Ok(data);
		}


		[HttpPost]
		public async Task<IActionResult> PlusIcon(int id)
		{
			var data = await _cartService.PlusIcon(id);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> MinusIcon(int id)
		{
			var data = await _cartService.MinusIcon(id);
			return Ok(data);
		}

	}
}

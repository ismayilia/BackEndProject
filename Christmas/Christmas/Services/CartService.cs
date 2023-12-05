using Christmas.Areas.Admin.ViewModels.Cart;
using Christmas.Data;
using Christmas.Helpers.Responses;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Christmas.Services
{
    public class CartService : ICartService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor,
                            AppDbContext context, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _productService = productService;   
        }
        public async Task<DeleteBasketResponse> DeleteData(int? id)
        {
			List<decimal> grandTotal = new();
			var basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = basket.FirstOrDefault(b => b.ProductId == id);
            basket.Remove(deletedProduct);

			foreach (var item in basket)
			{
				var product = await _productService.GetByIdAsync(item.ProductId);

				decimal productPrice = product.Price;

				decimal total = item.Count * productPrice;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			return new DeleteBasketResponse
			{
				Count = basket.Sum(m => m.Count),
				GrandTotal = grandTotal.Sum()
			};


		}

        public async Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.CartProducts.Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

		public int GetCount()
		{
			List<CartVM> basket;

			if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
			{
				basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			}
			else
			{
				basket = new List<CartVM>();

			}

			return basket.Sum(m => m.Count);
		}

		public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

		public async Task<IconBasketPlusAndMinus> MinusIcon(int id)
		{
			List<decimal> grandTotal = new();

			List<CartVM> basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			CartVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);


			if (existProduct.Count > 1)
			{

				existProduct.Count--;


			}
			foreach (var item in basket)
			{

				var product = await _productService.GetByIdAsync(item.ProductId);

				decimal total = item.Count * product.Price;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			var basketItem = await _productService.GetByIdAsync(id);
			var productGrandTotal = existProduct.Count * basketItem.Price;

			return new IconBasketPlusAndMinus
			{
				CountItem = existProduct.Count,
				BasketGrandTotal = grandTotal.Sum(),
				ProductGrandTotal = productGrandTotal,
				CountBasket = basket.Sum(m => m.Count)
			};

		}

		public async Task<IconBasketPlusAndMinus> PlusIcon(int id)
		{
			List<decimal> grandTotal = new();

			List<CartVM> basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			CartVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);
			existProduct.Count++;
			var basketItem = await _productService.GetByIdAsync(id);
			var productGrandTotal = existProduct.Count * basketItem.Price;

			foreach (var item in basket)
			{

				var product = await _productService.GetByIdAsync(item.ProductId);

				decimal total = item.Count * product.Price;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			return new IconBasketPlusAndMinus
			{
				CountItem = existProduct.Count,
				BasketGrandTotal = grandTotal.Sum(),
				ProductGrandTotal = productGrandTotal,



			};
		}

		public void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("basket", JsonConvert.SerializeObject(carts));
        }
    }
}

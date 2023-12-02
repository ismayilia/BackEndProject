using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Data;
using Christmas.Helpers;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Controllers
{
    public class ShopController : Controller
    {
		private readonly IProductService _productService;
		private readonly AppDbContext _context;

        public ShopController(IProductService productService,
			                  AppDbContext context)
		{
			_productService = productService;
			_context = context;
		}

		
		public async Task<IActionResult> Index(int page = 1, int take = 3)
		{
			List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

			int pageCount = await GetPageCountAsync(take);

			Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

			return View(paginatedDatas);
		}


		private async Task<int> GetPageCountAsync(int take)
		{
			int productCount = await _productService.GetCountAsync();
			return (int)Math.Ceiling((decimal)(productCount) / take);
		}

        public async Task<IActionResult> Search(string searchText)
        {
            var products = await _context.Products
            .Include(m => m.Images)
            .Include(m => m.Category)?
            .OrderByDescending(m => m.Id)
            .Where(m => !m.SoftDeleted && m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
            .Take(6)
            .ToListAsync();

            return View(products);
        }
    }
}

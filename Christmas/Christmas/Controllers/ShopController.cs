﻿using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Helpers;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Controllers
{
    public class ShopController : Controller
    {
		private readonly IProductService _productService;

		public ShopController(IProductService productService)
		{
			_productService = productService;
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
	}
}

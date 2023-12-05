using Christmas.Areas.Admin.ViewModels.Account;
using Christmas.Areas.Admin.ViewModels.Cart;
using Christmas.Areas.Admin.ViewModels.Wishlist;
using Christmas.Data;
using Christmas.Helpers.Enums;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace Christmas.Controllers
{
	public class AccountController : Controller
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ICartService _cartService;
		private readonly IWishlistService _wishlistService;
		private readonly AppDbContext _context;

		public AccountController(UserManager<AppUser> userManager,
								 SignInManager<AppUser> signInManager,
								 RoleManager<IdentityRole> roleManager,
								 IWishlistService wishlistService,
								 ICartService cartService,
								 AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_wishlistService = wishlistService;
			_cartService = cartService;
			_context = context;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			AppUser user = new()
			{
				FullName = request.FullName,
				Email = request.Email,
				UserName = request.Username,

			};

			IdentityResult result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
				}
				return View(request);

			}

			var createdUser = await _userManager.FindByNameAsync(user.UserName);

			await _userManager.AddToRoleAsync(createdUser, Roles.Member.ToString());

			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Login(LoginVM request)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View();
		//	}

		//	AppUser dbUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

		//	if (dbUser is null)
		//	{
		//		dbUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
		//	}

		//	if (dbUser is null)
		//	{
		//		ModelState.AddModelError(string.Empty, "Login informations is wrong");
		//		return View();
		//	}

		//	var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, false, false);
		//	//singingmanager logini o edir///false remember me mentiqi--checkboks qoymaq olur deyerin true ve ya false olur
		//	///lockout aktiv olsun ya yox
		//	if (!result.Succeeded)
		//	{
		//		ModelState.AddModelError(string.Empty, "Login informations is wrong");
		//		return View();

		//	}


		//	return RedirectToAction("Index", "Home");
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM model)
		{
			try
			{
				if (!ModelState.IsValid) return View(model);

				AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
				if (user == null)
				{
					user = await _userManager.FindByNameAsync(model.EmailOrUsername);
				}

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Email or password is wrong");
					if (!ModelState.IsValid) return View(model);
				}

				var res = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

				if (!res.Succeeded)
				{
					ModelState.AddModelError(string.Empty, "Email or password is wrong");
					return View(model);
				}

				List<CartVM> cartVMs = new();
				List<WishlistVM> wishlistVMs = new();

				Cart dbCart = await _cartService.GetByUserIdAsync(user.Id);
				Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(user.Id);

				if (dbCart is not null)
				{
					List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);

					foreach (var cartProduct in cartProducts)
					{
						cartVMs.Add(new CartVM
						{
							ProductId = cartProduct.ProductId,
							Count = cartProduct.Count
						});
					}

					Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
				}
				if (dbWishlist is not null)
				{
					List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);
					foreach (var wishlistProduct in wishlistProducts)
					{
						wishlistVMs.Add(new WishlistVM
						{
							ProductId = wishlistProduct.ProductId,
						});
					}
					Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));
				}
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ViewBag.error = ex.Message;
				return View();
			}
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Logout()
		//{
		//	await _signInManager.SignOutAsync();

		//	return RedirectToAction("Index", "Home");
		//}


		//Create Roles Method


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout(string userId)
		{
			await _signInManager.SignOutAsync();

			List<CartVM> carts = _cartService.GetDatasFromCookie();
			List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

			Cart dbCart = await _cartService.GetByUserIdAsync(userId);
			Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

			if (carts.Count != 0)
			{
				if (dbCart == null)
				{
					dbCart = new()
					{
						AppUserId = userId,
						CartProducts = new List<CartProduct>()
					};
					foreach (var cart in carts)
					{
						dbCart.CartProducts.Add(new CartProduct()
						{
							ProductId = cart.ProductId,
							CartId = dbCart.Id,
							Count = cart.Count
						});
					}
					await _context.Carts.AddAsync(dbCart);
					await _context.SaveChangesAsync();
				}
				else
				{
					List<CartProduct> cartProducts = new List<CartProduct>();
					foreach (var cart in carts)
					{
						cartProducts.Add(new CartProduct()
						{
							ProductId = cart.ProductId,
							CartId = dbCart.Id,
							Count = cart.Count
						});
					}
					dbCart.CartProducts = cartProducts;
					_context.SaveChanges();
				}
				Response.Cookies.Delete("basket");
			}
			else
			{
				_context.Carts.Remove(dbCart);
			}


			if (wishlists.Count != 0)
			{
				if (dbWishlist == null)
				{
					dbWishlist = new()
					{
						AppUserId = userId,
						WishlistProducts = new List<WishlistProduct>()
					};
					foreach (var wishlist in wishlists)
					{
						dbWishlist.WishlistProducts.Add(new WishlistProduct()
						{
							ProductId = wishlist.ProductId,
							WishlistId = dbWishlist.Id,
						});
					}
					await _context.Wishlists.AddAsync(dbWishlist);
					await _context.SaveChangesAsync();
				}
				else
				{
					List<WishlistProduct> wishlistProducts = new List<WishlistProduct>();
					foreach (var wishlist in wishlists)
					{
						wishlistProducts.Add(new WishlistProduct()
						{
							ProductId = wishlist.ProductId,
							WishlistId = dbWishlist.Id,
						});
					}
					dbWishlist.WishlistProducts = wishlistProducts;
					_context.SaveChanges();

				}
				Response.Cookies.Delete("wishlist");
			}
			else
			{
				_context.Wishlists.Remove(dbWishlist);
			}


			return RedirectToAction("Index", "Home");
		}
		[HttpGet]
		public async Task<IActionResult> CreateRoles()
		{
			foreach (var role in Enum.GetValues(typeof(Roles)))
			{
				if (!await _roleManager.RoleExistsAsync(role.ToString()))
				{
					await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
				}
			}
			return Ok();
		}
	}
}

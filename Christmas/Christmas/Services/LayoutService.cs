using Christmas.Areas.Admin.ViewModels.Layout;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Christmas.Services
{
    public class LayoutService : ILayoutService
    {

        private readonly ISettingService _settingService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<AppUser> _userManager;
        private readonly ICartService _cartService;

		public LayoutService(ISettingService settingService,IHttpContextAccessor httpContextAccessor,
                                                            UserManager<AppUser> userManager, 
                                                            ICartService cartService)
        {
            _settingService = settingService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _cartService = cartService;
        }

        public FooterVM GetFooterDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new FooterVM()
            {
                Logo = settingDatas["FooterLogo"],
                Email = settingDatas["Email"],
                Phone = settingDatas["Phone"],
                Eax = settingDatas["Eax"],
                Address = settingDatas["Address"]

            };
        }

        public async Task<HeaderVM> GetHeaderDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();
			int basketCount = _cartService.GetCount();

			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId is not null)
			{
				AppUser currentUser = await _userManager.FindByIdAsync(userId);
				return new HeaderVM()
				{
					Logo = settingDatas["HeaderLogo"],
                    FullName = currentUser.FullName,
                    BasketCount= basketCount

				};
			}
			return  new HeaderVM()
            {
                Logo = settingDatas["HeaderLogo"],

				BasketCount = 0
			};
        }
    }
}

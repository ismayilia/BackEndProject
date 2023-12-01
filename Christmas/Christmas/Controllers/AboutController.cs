using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Data;
using Christmas.Services;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutService _aboutService;
        private readonly IBrandService _brandService;
        private readonly ITeamService _teamService;


        public AboutController(AppDbContext context, IAboutService aboutService, 
                                                     IBrandService brandService, 
                                                     ITeamService teamService)

        {
            _context = context;
            _aboutService = aboutService;
            _brandService = brandService;
            _teamService = teamService;
            _brandService = brandService;
            _teamService = teamService;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM about = await _aboutService.GetDataAsync();
            List<TeamVM> teams = await _teamService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();

            AboutPageVM model = new()
            {
                About = about,
                Teams = teams,
                Brands = brands
            };

            return View(model);
        }
    }
}

using AutoMapper;
using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public TeamController(ITeamService teamService,
                              IWebHostEnvironment env,
                              IMapper mapper,
                              AppDbContext context)
        {
            _teamService = teamService;
            _env = env;
            _mapper = mapper;
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _teamService.GetAllAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            TeamVM dbTeam = await _teamService.GetByIdAsync((int)id);

            if (dbTeam is null) return NotFound();

            return View(dbTeam);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photos", "File size can be max 200 kb");
                return View();
            }



            await _teamService.CreateAsync(request);


            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (team is null) return NotFound();

            TeamEditVM advertEditVM = _mapper.Map<TeamEditVM>(team);


            return View(advertEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamEditVM request)
        {

            if (id is null) return BadRequest();

            Team dbTeam = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbTeam is null) return NotFound();


            request.Image = dbTeam.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbTeam);
                _context.Teams.Update(dbTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(request);
                }
            }


            await _teamService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}

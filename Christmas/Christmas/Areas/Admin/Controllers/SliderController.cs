using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public SliderController(AppDbContext context,
                                ISliderService sliderService,
                                IWebHostEnvironment env,
                                IMapper mapper)
        {
            _context = context;
            _sliderService = sliderService;
            _env = env;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sliderService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "File can be only image format");
                return View();
            }

            if (!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photos", "File size can be max 200 kb");
                return View();
            }



            await _sliderService.CreateAsync(slider);


            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m=>m.Id==id);

            if (slider is null) return NotFound();

            SliderEditVM sliderEditVM = _mapper.Map<SliderEditVM>(slider);
            return View(sliderEditVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderEditVM slider, int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider =await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();

            slider.Image = dbSlider.Image;

            if (!ModelState.IsValid)
            {
                return View(slider);
            }


            if (slider.Photo is null)
            {
                _mapper.Map(slider, dbSlider);
                _context.Sliders.Update(dbSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(slider);
                }

                if (!slider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(slider);
                }
            }
            await _sliderService.EditAsync(slider);
            return RedirectToAction(nameof(Index));
        }
    }
}

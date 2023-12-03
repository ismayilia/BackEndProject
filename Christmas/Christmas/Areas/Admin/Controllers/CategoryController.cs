using Christmas.Areas.Admin.ViewModels.Category;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Christmas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllVMAsync());
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdWithoutTrackingAsync((int)id);

            if (dbCategory is null) return NotFound();

            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);

            if (existCategory != null)
            {
                ModelState.AddModelError("Name", "This Tag already exists");
                return View();
            }


            await _categoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdWithoutTrackingAsync((int)id);

            if (dbCategory is null) return NotFound();

            return View(new CategoryEditVM
            {
                Name = dbCategory.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdWithoutTrackingAsync((int)id);

            if (dbCategory is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);


            if (existCategory != null)
            {
                if (existCategory.Id == request.Id)
                {
                    await _categoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This Tag already exists");
                return View();
            }

            await _categoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}

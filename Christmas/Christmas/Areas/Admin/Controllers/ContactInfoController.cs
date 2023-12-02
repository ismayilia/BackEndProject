using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Data;
using Christmas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactInfoController : Controller
    {
        private readonly AppDbContext _context;
        public ContactInfoController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _context.ContactInfos.FirstOrDefaultAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            ContactInfo dbContact = await _context.ContactInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (dbContact is null) return NotFound();

            return View(dbContact);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            ContactInfo dbContact = await _context.ContactInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (dbContact is null) return NotFound();


            return View(dbContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ContactInfo request)
        {

            if (id is null) return BadRequest();

            ContactInfo dbContact = await _context.ContactInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbContact is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (request.Desc == dbContact.Desc)
            {

                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.ContactInfos.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

        }
    }
}

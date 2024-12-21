using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKuaforProje.Models;

namespace WebKuaforProje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Randevu Listeleme (Index)
        public async Task<IActionResult> Index()
        {
            var randevular = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .ToListAsync();
            return View(randevular);
        }

        // 2. Randevu Oluşturma (Create) - GET
        public IActionResult Create()
        {
            ViewData["MusteriID"] = new SelectList(_context.Musteriler, "MusteriID", "FullName");
            ViewData["CalisanID"] = new SelectList(_context.Calisanlar, "CalisanID", "FullName");
            return View();
        }

        // 3. Randevu Oluşturma (Create) - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Randevu model)
        {
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevunuz başarıyla alındı!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["MusteriID"] = new SelectList(_context.Musteriler, "MusteriID", "FullName", model.MusteriID);
            ViewData["CalisanID"] = new SelectList(_context.Calisanlar, "CalisanID", "FullName", model.CalisanID);
            TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View(model);
        }

        // 4. Randevu Silme (Delete) - GET
        public async Task<IActionResult> Delete(int id)
        {
            var randevu = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .FirstOrDefaultAsync(r => r.RandevuID == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // 5. Randevu Silme (Delete) - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }

        // 6. Randevu Düzenleme (Edit) - GET
        public async Task<IActionResult> Edit(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            ViewData["MusteriID"] = new SelectList(_context.Musteriler, "MusteriID", "FullName", randevu.MusteriID);
            ViewData["CalisanID"] = new SelectList(_context.Calisanlar, "CalisanID", "FullName", randevu.CalisanID);

            return View(randevu);
        }

        // 7. Randevu Düzenleme (Edit) - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Randevu model)
        {
            if (id != model.RandevuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Randevu başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Randevular.Any(e => e.RandevuID == model.RandevuID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["MusteriID"] = new SelectList(_context.Musteriler, "MusteriID", "FullName", model.MusteriID);
            ViewData["CalisanID"] = new SelectList(_context.Calisanlar, "CalisanID", "FullName", model.CalisanID);

            TempData["ErrorMessage"] = "Bir hata oluştu. Lütfen tekrar deneyin.";
            return View(model);
        }

        // 8. Randevu Detayları (Details) - GET
        public async Task<IActionResult> Details(int id)
        {
            var randevu = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .FirstOrDefaultAsync(r => r.RandevuID == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
    }
}

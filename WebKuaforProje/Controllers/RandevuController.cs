using Microsoft.AspNetCore.Mvc;
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

        // GET: Randevu/Create
        public IActionResult Create()
        {
            ViewBag.Musteriler = _context.Musteriler.ToList(); // Müşterileri ViewBag'e ekliyoruz
            return View();
        }

        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Randevu model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Randevu listesine yönlendirilebilir
            }
            ViewBag.Musteriler = _context.Musteriler.ToList(); // Eğer model geçerli değilse, müşteri listesini tekrar gönderiyoruz
            return View(model);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.Include(r => r.Musteri).FirstOrDefaultAsync(r => r.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewBag.Musteriler = _context.Musteriler.ToList(); // Müşteri listesini ViewBag'e ekliyoruz
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Randevu model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Randevu listesine yönlendirilebilir
            }
            ViewBag.Musteriler = _context.Musteriler.ToList(); // Eğer model geçerli değilse, müşteri listesini tekrar gönderiyoruz
            return View(model);
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.Id == id);
        }
    }
}

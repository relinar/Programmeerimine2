using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NutrientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nutrients
        public async Task<IActionResult> Index()
        {
            return View(await _context.nutrients.ToListAsync());
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await _context.nutrients
                .FirstOrDefaultAsync(m => m.NutrientsID == id);
            if (nutrients == null)
            {
                return NotFound();
            }

            return View(nutrients);
        }

        // GET: Nutrients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NutrientsID,Name,Carbohydrates,Sugars,Fats,FoodChart")] Nutrients nutrients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nutrients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nutrients);
        }

        // GET: Nutrients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await _context.nutrients.FindAsync(id);
            if (nutrients == null)
            {
                return NotFound();
            }
            return View(nutrients);
        }

        // POST: Nutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NutrientsID,Name,Carbohydrates,Sugars,Fats,FoodChart")] Nutrients nutrients)
        {
            if (id != nutrients.NutrientsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nutrients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutrientsExists(nutrients.NutrientsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nutrients);
        }

        // GET: Nutrients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await _context.nutrients
                .FirstOrDefaultAsync(m => m.NutrientsID == id);
            if (nutrients == null)
            {
                return NotFound();
            }

            return View(nutrients);
        }

        // POST: Nutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nutrients = await _context.nutrients.FindAsync(id);
            if (nutrients != null)
            {
                _context.nutrients.Remove(nutrients);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutrientsExists(int id)
        {
            return _context.nutrients.Any(e => e.NutrientsID == id);
        }
    }
}

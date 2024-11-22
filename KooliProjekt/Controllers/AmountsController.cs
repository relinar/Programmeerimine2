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
    public class AmountsController : Controller
    {
        private readonly ApplicationDbContext amountService;

        public AmountsController(ApplicationDbContext context)
        {
            amountService = context;
        }

        // GET: Amounts
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await amountService.amount.GetPagedAsync(page, 5));
        }

        // GET: Amounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await amountService.amount
                .FirstOrDefaultAsync(m => m.AmountID == id);
            if (amount == null)
            {
                return NotFound();
            }

            return View(amount);
        }

        // GET: Amounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmountID,NutrientsID,AmountDate")] Amount amount)
        {
            if (ModelState.IsValid)
            {
                amountService.Add(amount);
                await amountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amount);
        }

        // GET: Amounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await amountService.amount.FindAsync(id);
            if (amount == null)
            {
                return NotFound();
            }
            return View(amount);
        }

        // POST: Amounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmountID,NutrientsID,AmountDate")] Amount amount)
        {
            if (id != amount.AmountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    amountService.Update(amount);
                    await amountService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmountExists(amount.AmountID))
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
            return View(amount);
        }

        // GET: Amounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await amountService.amount
                .FirstOrDefaultAsync(m => m.AmountID == id);
            if (amount == null)
            {
                return NotFound();
            }

            return View(amount);
        }

        // POST: Amounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amount = await amountService.amount.FindAsync(id);
            if (amount != null)
            {
                amountService.amount.Remove(amount);
            }

            await amountService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmountExists(int id)
        {
            return amountService.amount.Any(e => e.AmountID == id);
        }
    }
}

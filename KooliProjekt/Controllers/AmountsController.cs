﻿using System;
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
        private readonly ApplicationDbContext _context;

        public AmountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Amounts
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _context.amount.GetPagedAsync(page, 5));
        }

        // GET: Amounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await _context.amount
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
                _context.Add(amount);
                await _context.SaveChangesAsync();
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

            var amount = await _context.amount.FindAsync(id);
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
                    _context.Update(amount);
                    await _context.SaveChangesAsync();
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

            var amount = await _context.amount
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
            var amount = await _context.amount.FindAsync(id);
            if (amount != null)
            {
                _context.amount.Remove(amount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmountExists(int id)
        {
            return _context.amount.Any(e => e.AmountID == id);
        }
    }
}
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
    public class FoodChartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodChartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: food_chart
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _context.food_Chart.GetPagedAsync(page, 5));
        }

        // GET: food_chart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food_chart = await _context.food_Chart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food_chart == null)
            {
                return NotFound();
            }

            return View(food_chart);
        }

        // GET: food_chart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: food_chart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceNo,InvoiceDate,user,date,meal,nutrients,amount")] FoodChart food_chart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food_chart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food_chart);
        }

        // GET: food_chart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food_chart = await _context.food_Chart.FindAsync(id);
            if (food_chart == null)
            {
                return NotFound();
            }
            return View(food_chart);
        }

        // POST: food_chart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNo,InvoiceDate,user,date,meal,nutrients,amount")] FoodChart food_chart)
        {
            if (id != food_chart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food_chart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!food_chartExists(food_chart.Id))
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
            return View(food_chart);
        }

        // GET: food_chart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food_chart = await _context.food_Chart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food_chart == null)
            {
                return NotFound();
            }

            return View(food_chart);
        }

        // POST: food_chart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food_chart = await _context.food_Chart.FindAsync(id);
            if (food_chart != null)
            {
                _context.food_Chart.Remove(food_chart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool food_chartExists(int id)
        {
            return _context.food_Chart.Any(e => e.Id == id);
        }
    }
}
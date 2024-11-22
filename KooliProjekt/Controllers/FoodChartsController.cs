using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class FoodChartsController : Controller
    {
        private readonly ApplicationDbContext FoodChartService;

        public FoodChartsController(ApplicationDbContext context)
        {
            FoodChartService = context;
        }

        // GET: food_chart
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await FoodChartService.food_Chart.GetPagedAsync(page, 5));
        }

        // GET: food_chart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food_chart = await FoodChartService.food_Chart
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
                FoodChartService.Add(food_chart);
                await FoodChartService.SaveChangesAsync();
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

            var food_chart = await FoodChartService.food_Chart.FindAsync(id);
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
                    FoodChartService.Update(food_chart);
                    await FoodChartService.SaveChangesAsync();
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

            var food_chart = await FoodChartService.food_Chart
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
            var food_chart = await FoodChartService.food_Chart.FindAsync(id);
            if (food_chart != null)
            {
                FoodChartService.food_Chart.Remove(food_chart);
            }

            await FoodChartService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool food_chartExists(int id)
        {
            return FoodChartService.food_Chart.Any(e => e.Id == id);
        }
    }
}

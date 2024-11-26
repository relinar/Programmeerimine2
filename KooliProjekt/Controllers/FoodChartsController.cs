using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class FoodChartsController : Controller
    {
        private readonly IFoodChartService _foodChartService;

        public FoodChartsController(IFoodChartService foodChartService)
        {
            _foodChartService = foodChartService;
        }

        // GET: food_chart
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _foodChartService.List(page, 5);

            return View(model);
        }

        // GET: food_chart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food_chart = await _foodChartService.Get(id.Value);
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
                await _foodChartService.Save(food_chart);
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

            var food_chart = await _foodChartService.Get(id.Value);
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
                await _foodChartService.Save(food_chart);
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

            var food_chart = await _foodChartService.Get(id.Value);
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
            await _foodChartService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
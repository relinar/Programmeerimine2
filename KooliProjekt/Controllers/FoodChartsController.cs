using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class FoodChartsController : Controller
    {
        private readonly IFoodChartService _foodChartService;

        // Constructor for dependency injection
        public FoodChartsController(IFoodChartService foodChartService)
        {
            _foodChartService = foodChartService;
        }

        // GET: FoodCharts/Index
        public async Task<IActionResult> Index(int page = 1, FoodChartIndexModel model = null)
        {
            model = model ?? new FoodChartIndexModel();

            // Fetch paginated results
            var pagedResult = await _foodChartService.List(page, 5, model.Search);

            // Return the paged result directly, since the view expects this type
            return View(pagedResult);
        }


        // GET: FoodCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodChart = await _foodChartService.Get(id.Value);
            if (foodChart == null)
            {
                return NotFound();
            }

            return View(foodChart);
        }

        // GET: FoodCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCharts/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodChart foodChart)
        {
            if (ModelState.IsValid)
            {
                await _foodChartService.Save(foodChart);  // Save the new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(foodChart);
        }

        // GET: FoodCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodChart = await _foodChartService.Get(id.Value);
            if (foodChart == null)
            {
                return NotFound();
            }
            return View(foodChart);
        }

        // POST: FoodCharts/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNo,InvoiceDate,user,date,meal,nutrients,amount")] FoodChart foodChart)
        {
            if (id != foodChart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _foodChartService.Save(foodChart);  // Save the updated food chart
                return RedirectToAction(nameof(Index));
            }
            return View(foodChart);
        }

        // GET: FoodCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodChart = await _foodChartService.Get(id.Value);
            if (foodChart == null)
            {
                return NotFound();
            }

            return View(foodChart);
        }

        // POST: FoodCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodChart = await _foodChartService.Get(id);
            if (foodChart == null)
            {
                return NotFound();
            }

            await _foodChartService.Delete(id);  // Delete the food chart
            return RedirectToAction(nameof(Index));
        }
    }
}

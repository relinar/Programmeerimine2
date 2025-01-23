using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsRepository _nutrientsRepository;

        // Constructor for dependency injection
        public NutrientsController(INutrientsRepository nutrientsRepository)
        {
            _nutrientsRepository = nutrientsRepository;
        }

        // GET: Nutrients/Index
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, NutrientsSearch nutrientsSearch = null)
        {
            // If no search parameters, initialize to empty search
            nutrientsSearch = nutrientsSearch ?? new NutrientsSearch();

            // Fetch paginated results based on the search criteria
            var nutrientsResult = await _nutrientsRepository.List(page, pageSize, nutrientsSearch);

            // Prepare the view model with search and results data
            var viewModel = new NutrientsIndexModel
            {
                Search = nutrientsSearch,
                Data = nutrientsResult
            };

            return View(viewModel);
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsRepository.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

        // GET: Nutrients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nutrients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Carbohydrates, Sugars, Fats")] Nutrients nutrient)
        {
            if (ModelState.IsValid)
            {
                await _nutrientsRepository.Save(nutrient);  // Save new nutrient
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }

        // GET: Nutrients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsRepository.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }
            return View(nutrient);
        }

        // POST: Nutrients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Carbohydrates, Sugars, Fats")] Nutrients nutrient)
        {
            if (id != nutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nutrientsRepository.Save(nutrient);  // Save updated nutrient
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }

        // GET: Nutrients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsRepository.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

        // POST: Nutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nutrientsRepository.Delete(id);  // Delete nutrient
            return RedirectToAction(nameof(Index));
        }
    }
}
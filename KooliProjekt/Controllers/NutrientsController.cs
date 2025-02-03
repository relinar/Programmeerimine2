using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsService _nutrientsService;
        private readonly INutrientsRepository _nutrientsRepository; // Declare the repository field

        // Inject both service and repository via constructor
        public NutrientsController(INutrientsService nutrientsService, INutrientsRepository nutrientsRepository)
        {
            _nutrientsService = nutrientsService;
            _nutrientsRepository = nutrientsRepository; // Initialize the repository
        }

        // Example action methods using the repository
        public async Task<IActionResult> Index(int page = 1)
        {
            var search = new NutrientsSearch
            {
                Carbohydrates = "10",
                Fats = "3",
                Name = "Test Nutrient",
                Sugars = "5"
            };

            // Now, you can use _nutrientsRepository here if needed
            var pagedResult = await _nutrientsService.List(page, 10, search);
            return View(pagedResult); // Pass the result to the view
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
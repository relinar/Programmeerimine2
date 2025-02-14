using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsService _nutrientsService;

        // Constructor to inject the service
        public NutrientsController(INutrientsService nutrientsService)
        {
            _nutrientsService = nutrientsService;
        }

        public async Task<IActionResult> Index(int page = 1, NutrientsSearch search = null)
        {
            // Query the service to get the paged nutrient data
            var pagedResult = await _nutrientsService.List(page, 10, search);

            // Create the model for the view
            var model = new NutrientsIndexModel
            {
                Data = pagedResult,
                Search = search
            };

            // Return the view with the model
            return View(model);
        }

        // Create (GET) - Returns the view to create a new nutrient
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST) - Creates a new nutrient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nutrients nutrient)
        {
            if (ModelState.IsValid)
            {
                await _nutrientsService.Add(nutrient);
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }

        // Edit (GET) - Returns the view to edit an existing nutrient
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

       
        public async Task<IActionResult> Edit(int id, Nutrients nutrient)
        {
            if (id != nutrient.Id)
            {
                return NotFound();
            }

            var existingNutrient = await _nutrientsService.Get(id);
            if (existingNutrient == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nutrientsService.Update(nutrient);
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }


        // Delete (GET) - Returns the view to confirm deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

        // Delete (POST) - Deletes the nutrient
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nutrientsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Details (GET) - Returns the details view for a specific nutrient
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }
    }
}

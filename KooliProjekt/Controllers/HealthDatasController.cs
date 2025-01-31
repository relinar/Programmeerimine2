using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;
using KooliProjekt.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class HealthDatasController : Controller
    {
        private readonly IHealthDataService _healthDataService;

        public HealthDatasController(IHealthDataService healthDataService)
        {
            _healthDataService = healthDataService;
        }

        // Index Action: Lists all health data records
        public async Task<IActionResult> Index()
        {
            // Retrieve all health data records (mocked here for simplicity)
            var healthDataList = new List<HealthData>
            {
                await _healthDataService.Get(1),
                await _healthDataService.Get(2)
            };
            return View(healthDataList);
        }

        // Create Action: Displays the form for creating a new health data record (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create Action: Handles creating a new health data record (POST)
        [HttpPost]
        public async Task<IActionResult> Create(HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                await _healthDataService.Add(healthData);
                return RedirectToAction("Index");
            }
            return View(healthData);
        }

        // Edit Action: Shows the edit view for a health data record (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
                return NotFound();
            return View(healthData);
        }

        // Edit Action: Handles updating a health data record (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, HealthData healthData)
        {
            if (id != healthData.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _healthDataService.Update(healthData);
                return RedirectToAction("Index");
            }
            return View(healthData);
        }

        // Delete Action: Deletes a health data record (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
                return NotFound();
            return View(healthData);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
            {
                // Log missing HealthData
                Console.WriteLine($"HealthData with ID {id} not found.");
                return NotFound();
            }

            await _healthDataService.Delete(id);  // Delete the health data
            return RedirectToAction("Index");
        }


        // Details Action: Displays details of a specific health data record
        public async Task<IActionResult> Details(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
                return NotFound();
            return View(healthData);
        }
    }
}

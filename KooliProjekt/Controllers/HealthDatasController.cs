using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;
using KooliProjekt.Models;
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

        // Edit Action: Shows the edit view for a health data record
        public async Task<IActionResult> Edit(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
                return NotFound();
            return View(healthData);
        }

        // Add Action: Handles adding new health data
        [HttpPost]
        public async Task<IActionResult> Add(HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                await _healthDataService.Add(healthData);
                return RedirectToAction("Index");
            }
            return View(healthData);
        }

        // Delete Action: Deletes a health data record
        public async Task<IActionResult> Delete(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
                return NotFound();
            return View(healthData);
        }
    }
}

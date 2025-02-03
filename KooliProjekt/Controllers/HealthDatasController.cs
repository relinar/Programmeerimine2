using System.Threading.Tasks;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.AspNetCore.Mvc;
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

        // GET: HealthDatas/Index
        public async Task<IActionResult> Index(int page = 1, HealthDataSearch searchModel = null)
        {
            searchModel ??= new HealthDataSearch(); // Initialize search model if null
            var pagedResult = await _healthDataService.List(page, 5, searchModel);
            return View(pagedResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                await _healthDataService.AddAsync(healthData);
                await _healthDataService.SaveAsync(); // No arguments
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _healthDataService.DeleteAsync(id);
            await _healthDataService.SaveAsync(); // No arguments
            return RedirectToAction(nameof(Index));
        }
    }
}

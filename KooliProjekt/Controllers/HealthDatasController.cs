// File: Controllers/HealthDatasController.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            searchModel = searchModel ?? new HealthDataSearch(); // If no search model is passed, create a new one

            // Fetch paginated health data based on the search model
            var pagedResult = await _healthDataService.List(page, 5, searchModel);

            // Return the view with the paged results
            return View(pagedResult);
        }
    }
}

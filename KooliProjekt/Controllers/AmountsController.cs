using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class AmountsController : Controller
    {
        private readonly IAmountService _amountService;

        // Constructor for dependency injection
        public AmountsController(IAmountService amountService)
        {
            _amountService = amountService;
        }

        // GET: Amounts/Index
        public async Task<IActionResult> Index(int page = 1, AmountIndexModel model = null)
        {
            model = model ?? new AmountIndexModel();
            model.Data = await _amountService.List(page, 5, model.Search);  // Fetch paginated results

            return View(model);
        }

        // GET: Amounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await _amountService.Get(id.Value);
            if (amount == null)
            {
                return NotFound();
            }

            return View(amount);
        }

        // GET: Amounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmountID, NutrientsID, AmountDate, AmountValue")] Amount amount)
        {
            if (ModelState.IsValid)
            {
                await _amountService.Save(amount);  // Save new amount
                return RedirectToAction(nameof(Index));
            }
            return View(amount);
        }

        // GET: Amounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await _amountService.Get(id.Value);
            if (amount == null)
            {
                return NotFound();
            }

            return View(amount);
        }

        // POST: Amounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmountID, NutrientsID, AmountDate, AmountValue")] Amount amount)
        {
            if (id != amount.AmountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _amountService.Save(amount);  // Save updated amount
                return RedirectToAction(nameof(Index));
            }
            return View(amount);
        }

        // GET: Amounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amount = await _amountService.Get(id.Value);
            if (amount == null)
            {
                return NotFound();
            }

            return View(amount);
        }

        // POST: Amounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _amountService.Delete(id);  // Delete amount
            return RedirectToAction(nameof(Index));
        }
    }
}

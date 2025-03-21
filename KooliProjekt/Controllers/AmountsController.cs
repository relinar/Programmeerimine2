﻿// AmountsController.cs
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

        // Constructor injection for IAmountService
        public AmountsController(IAmountService amountService)
        {
            _amountService = amountService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var amount = await _amountService.Get(id);
            if (amount == null)
            {
                return NotFound();
            }
            return View(amount);  
        }


        public async Task<IActionResult> Index(int page = 1)
        {
           
            var Data = await _amountService.List(page, 5, null);
           
            return View(Data);
        }



        // Create Action: Add a new amount
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Amount amount)
        {
            if (ModelState.IsValid)
            {
                await _amountService.AddAmountAsync(amount);
                return RedirectToAction(nameof(Index));
            }
            return View(amount);
        }

        // Edit Action: Edit an existing amount
        public async Task<IActionResult> Edit(int id)
        {
            var amount = await _amountService.Get(id);
            if (amount == null)
            {
                return NotFound();
            }
            return View(amount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Amount amount)
        {
            if (ModelState.IsValid)
            {
                await _amountService.UpdateAmountAsync(amount);
                return RedirectToAction(nameof(Index));
            }
            return View(amount);
        }

        // Delete Action: Delete an amount
        public async Task<IActionResult> Delete(int id)
        {
            var amount = await _amountService.Get(id);
            if (amount == null)
            {
                return NotFound();
            }
            return View(amount);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _amountService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

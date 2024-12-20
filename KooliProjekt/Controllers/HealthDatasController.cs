﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;    
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class HealthDatasController : Controller
    {
        private readonly ApplicationDbContext healthdataService;

        public HealthDatasController(ApplicationDbContext context)
        {
            healthdataService = context;
        }

        // GET: HealthDatas
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await healthdataService.health_data.GetPagedAsync(page, 5));
        }

        // GET: HealthDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await healthdataService.health_data
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // GET: HealthDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HealthDataID,User,Date,BloodSugar,Weight,BloodAir,Systolic,Diastolic,Pulse")] HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                healthdataService.Add(healthData);
                await healthdataService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: HealthDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await healthdataService.health_data.FindAsync(id);
            if (healthData == null)
            {
                return NotFound();
            }
            return View(healthData);
        }

        // POST: HealthDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HealthDataID,User,Date,BloodSugar,Weight,BloodAir,Systolic,Diastolic,Pulse")] HealthData healthData)
        {
            if (id != healthData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    healthdataService.Update(healthData);
                    await healthdataService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthDataExists(healthData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: HealthDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await healthdataService.health_data
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // POST: HealthDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthData = await healthdataService.health_data.FindAsync(id);
            if (healthData != null)
            {
                healthdataService.health_data.Remove(healthData);
            }

            await healthdataService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthDataExists(int id)
        {
            return healthdataService.health_data.Any(e => e.Id == id);
        }
    }
}

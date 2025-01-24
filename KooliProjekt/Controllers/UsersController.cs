using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;  // Keep this as IUserService

        // Constructor to inject the IUserService
        public UsersController(IUserService userService)  // Change to inject IUserService
        {
            _userService = userService;  // Correct assignment
        }

        // GET: Users
        public async Task<IActionResult> Index(int page = 1, UserIndexModel model = null)
        {
            model = model ?? new UserIndexModel();
            model.Data = await _userService.List(page, 5, model.Search);  // Use IUserService's List method

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.Get(id.Value);  // Use IUserService's Get method
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: TodoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoLists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Save(user);  // Use IUserService's Save method
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: TodoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoList = await _userService.Get(id.Value);  // Use IUserService's Get method
            if (todoList == null)
            {
                return NotFound();
            }
            return View(todoList);
        }

        // POST: TodoLists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userService.Save(user);  // Use IUserService's Save method
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: TodoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoList = await _userService.Get(id.Value);  // Use IUserService's Get method
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        // POST: TodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);  // Use IUserService's Delete method
            return RedirectToAction(nameof(Index));
        }
    }
}

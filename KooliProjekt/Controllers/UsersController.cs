using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;
using KooliProjekt.Models;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1, UserIndexModel model = null)
        {
            try
            {
                model ??= new UserIndexModel();
                var pagedResult = await _userService.List(page, 5, model.Search);

                // Pass the paged result directly to the view (without wrapping it in UserIndexModel)
                return View(pagedResult);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while loading users.");
                return View(new UserIndexModel { Data = null, Search = null });
            }
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var user = await _userService.Get(id.Value);
                if (user == null) return NotFound();
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Role")] User user)
        {
            if (!ModelState.IsValid) return View(user);

            try
            {
                await _userService.Save(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving the user.");
                return View(user);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var user = await _userService.Get(id.Value);
                if (user == null) return NotFound();
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Role")] User user)
        {
            if (id != user.Id) return NotFound();

            if (!ModelState.IsValid) return View(user);

            try
            {
                await _userService.Save(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while updating the user.");
                return View(user);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var user = await _userService.Get(id.Value);
                if (user == null) return NotFound();
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while deleting the user.");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

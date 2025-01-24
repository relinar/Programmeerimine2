using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.Diagnostics;

namespace KooliProjekt.Controllers
{
    public class HomeController : Controller
    {
        // Konstruktorit ei ole, kuna ei kasuta enam ILogger-t
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()  
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

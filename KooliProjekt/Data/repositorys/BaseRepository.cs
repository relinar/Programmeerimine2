using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Data.repositorys
{
    public class BaseRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

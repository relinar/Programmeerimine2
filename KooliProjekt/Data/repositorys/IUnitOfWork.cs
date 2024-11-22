using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Data.repositorys
{
    public class IUnitOfWork : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

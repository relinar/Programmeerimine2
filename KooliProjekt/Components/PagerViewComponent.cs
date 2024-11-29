using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Models;
using KooliProjekt.Data;

namespace KooliProjekt.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PagedResultBase model)
        {
            return View(model);
        }
    }
}

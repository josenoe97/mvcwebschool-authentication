using Microsoft.AspNetCore.Mvc;

namespace MvcWebSchool_Identity.Areas.Admin.Controllers
{
    [Area("Admin")] // Necessário para acessar Admin !!!
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

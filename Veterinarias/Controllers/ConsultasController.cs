using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class ConsultasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

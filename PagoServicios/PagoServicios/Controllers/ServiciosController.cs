using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

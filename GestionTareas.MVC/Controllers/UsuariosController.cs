using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

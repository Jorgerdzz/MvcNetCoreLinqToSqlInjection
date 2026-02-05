using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class CochesController : Controller
    {
        private Coche coche;

        public CochesController(Coche coche)
        {
            this.coche = coche;
        }

        public IActionResult Index()
        {
            return View(this.coche);
        }

        [HttpPost]
        public IActionResult Index(string accion)
        {
            if(accion.ToLower() == "acelerar")
            {
                this.coche.Acelerar();
            }else
            {
                this.coche.Frenar();
            }
            return View(this.coche);
        }
    }
}

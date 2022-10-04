using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Sistema.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
    }
}

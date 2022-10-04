using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaModelo;
using CapaDatos;

namespace Sistema.Controllers
{
    public class ComprasController : Controller
    {
        public ActionResult Crear()
        {
            return View();
        }
    }
}

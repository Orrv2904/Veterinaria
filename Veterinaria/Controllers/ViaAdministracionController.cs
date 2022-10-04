using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Veterinaria.Controllers
{
    public class ViaAdministracionController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Obtener()
        {
            List<ViaAdministracion> lista = CD_ViaAdministracion.Instancia.ObtenerVia();
            return Json(new { data = lista });
        }

        public JsonResult Guardar(ViaAdministracion objeto)
        {
            bool respuesta = false;

            if (objeto.CodVia == 0)
            {

                respuesta = CD_ViaAdministracion.Instancia.RegistrarVia(objeto);
            }
            else
            {
                respuesta = CD_ViaAdministracion.Instancia.ModificarVia(objeto);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_ViaAdministracion.Instancia.EliminarVia(id);
            return Json(new { resultado = respuesta });
        }
    }
}

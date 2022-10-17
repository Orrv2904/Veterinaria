using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Veterinaria.Controllers
{
    public class BodegaController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            List<Bodega> lista = CD_Bodega.Instancia.ObtenerBodega();
            return Json(new { data = lista });
        }

        public JsonResult Guardar(Bodega objeto)
        {
            bool respuesta = false;

            if (objeto.CodBodega == 0)
            {

                respuesta = CD_Bodega.Instancia.RegistrarBodega(objeto);
            }
            else
            {
                respuesta = CD_Bodega.Instancia.ModificarBodega(objeto);

            }
            return Json(new { resultado = respuesta });
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Bodega.Instancia.EliminarBodega(id);
            return Json(new { resultado = respuesta });
        }
    }
}

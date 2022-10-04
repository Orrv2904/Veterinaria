using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaModelo;
using CapaDatos;


namespace Sistema.Controllers
{
    public class DetalleFarmacoController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        public JsonResult Obtener()
        {
            List<DetalleFarmaco> lista = CD_DetalleFarmaco.Instancia.ObtenerDetalleFarmaco();
            return Json(new { data = lista });
        }
        [HttpPost]
        public JsonResult Guardar(DetalleFarmaco objeto)
        {
            bool respuesta = false;

            if (objeto.CodDetalleFarmaco == 0)
            {

                respuesta = CD_DetalleFarmaco.Instancia.RegistrarDetalleFarmaco(objeto);
            }
            else
            {
                respuesta = CD_DetalleFarmaco.Instancia.ModificarDetalleFarmaco(objeto);
            }


            return Json(new { resultado = respuesta });
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_DetalleFarmaco.Instancia.EliminarDetalleFarmaco(id);

            return Json(new { resultado = respuesta });
        }
    }
}


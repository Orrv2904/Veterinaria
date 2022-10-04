using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Sistema.Controllers
{
    public class PresentacionController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            List<Presentacion> lista = CD_Presentacion.Instancia.ObtenerPresentacion();
            return Json(new { data = lista });
        }
        public JsonResult Guardar(Presentacion objeto)
        {
            bool respuesta = false;

            if (objeto.CodPresentacion == 0)
            {

                respuesta = CD_Presentacion.Instancia.RegistrarPresentacion(objeto);
            }
            else
            {
                respuesta = CD_Presentacion.Instancia.ModificarPresentacion(objeto);
            }
            return Json(new { resultado = respuesta });
        }
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Presentacion.Instancia.EliminarPresentacion(id);
            return Json(new { resultado = respuesta });
        }
    }
}

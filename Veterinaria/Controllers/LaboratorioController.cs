using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Sistema.Controllers
{
    public class LaboratorioController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Obtener()
        {
            List<Laboratorios> lista = CD_Laboratorio.Instancia.ObtenerLaboratorio();
            return Json(new { data = lista });
        }

        public JsonResult Guardar(Laboratorios objeto)
        {
            bool respuesta = false;

            if (objeto.CodLaboratorio == 0)
            {

                respuesta = CD_Laboratorio.Instancia.RegistrarLaboratorio(objeto);
            }
            else
            {
                respuesta = CD_Laboratorio.Instancia.ModificarLaboratorio(objeto);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Laboratorio.Instancia.EliminarLaboratorio(id);
            return Json(new { resultado = respuesta });
        }
    }
}

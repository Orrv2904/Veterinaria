using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaModelo;

namespace Sistema.Controllers
{
    public class ProveedorController : Controller
    {
        
            public IActionResult Crear()
            {
                return View();
            }

            [HttpGet]
            public JsonResult Obtener()
            {
                List<Proveedores> lista = CD_Proveedor.Instancia.ObtenerProveedor();
                return Json(new { data = lista });
            }
        public JsonResult Guardar(Proveedores objeto)
        {
            bool respuesta = false;

            if (objeto.CodProveedor == 0)
            {

                respuesta = CD_Proveedor.Instancia.RegistrarProveedor(objeto);
            }
            else
            {
                respuesta = CD_Proveedor.Instancia.ModificarProveedor(objeto);
            }
            return Json(new { resultado = respuesta });
        }
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Proveedor.Instancia.EliminarProveedor(id);
            return Json(new { resultado = respuesta });
        }
    }
}

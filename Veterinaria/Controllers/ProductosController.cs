using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaModelo;
using CapaDatos;

namespace Sistema.Controllers
{
    public class ProductosController : Controller
    {
        public ActionResult Crear()
        {
            return View();
        }
        // GET: Producto
        //public ActionResult Asignar()
        //{
        //    return View();
        //}



        public JsonResult Obtener()
        {
            List<Productos> lista = CD_Producto.Instancia.ObtenerProducto();
            return Json(new { data = lista });
        }
        [HttpPost]
        public JsonResult Guardar(Productos objeto)
        {
            bool respuesta = false;

            if (objeto.Id_Farmaco == 0)
            {

                respuesta = CD_Producto.Instancia.RegistrarProducto(objeto);
            }
            else
            {
                respuesta = CD_Producto.Instancia.ModificarProducto(objeto);
            }


            return Json(new { resultado = respuesta });
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Producto.Instancia.EliminarProducto(id);

            return Json(new { resultado = respuesta });
        }
    }
}

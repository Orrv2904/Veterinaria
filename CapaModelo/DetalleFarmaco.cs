using System;
using System.Collections.Generic;
using System.Text;

namespace CapaModelo
{
    public class DetalleFarmaco
    {
        public int CodDetalleFarmaco { get; set; }
        public string Concentracion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int CodLaboratorio { get; set; }
        public Laboratorios Objlaboratorio { get; set; }
        public int CodProveedor { get; set; }
        public Proveedores Objproveedor { get; set; }
        public int CodVia { get; set; }
        public ViaAdministracion ObjviaAdministracion { get; set; }
        public string NombreComercial { get; set; }
        public string NumeroLote { get; set; }
    }
}

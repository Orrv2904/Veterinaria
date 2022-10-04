using System;
using System.Collections.Generic;
using System.Text;

namespace CapaModelo
{
    public class Cliente
    {
        public int CodCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
    }
}

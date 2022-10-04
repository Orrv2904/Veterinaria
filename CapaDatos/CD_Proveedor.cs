using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Proveedor
    {
        public static CD_Proveedor _instancia = null;

        private CD_Proveedor()
        {

        }

        public static CD_Proveedor Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Proveedor();
                }
                return _instancia;
            }
        }

        public List<Proveedores> ObtenerProveedor()
        {
            List<Proveedores> rptListaProveedores = new List<Proveedores>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ProveedorObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProveedores.Add(new Proveedores()
                        {
                            CodProveedor = Convert.ToInt32(dr["CodProveedor"].ToString()),
                            Direccion = dr["Direccion"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            RazonSocial = dr["RazonSocial"].ToString(),
                            Ruc = dr["RUC"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                            
                        });
                    }
                    dr.Close();

                    return rptListaProveedores;

                }
                catch (Exception)
                {
                    rptListaProveedores = null;
                    return rptListaProveedores;
                }
            }
        }
        public bool RegistrarProveedor(Proveedores oProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarProveedores", oConexion);
                    cmd.Parameters.AddWithValue("codproveedor", oProveedor.CodProveedor);
                    cmd.Parameters.AddWithValue("direccion", oProveedor.Direccion);
                    cmd.Parameters.AddWithValue("correo", oProveedor.Correo);
                    cmd.Parameters.AddWithValue("razonsocial", oProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("ruc", oProveedor.Ruc);
                    cmd.Parameters.AddWithValue("Estado", oProveedor.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public bool ModificarProveedor(Proveedores oProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarProveedor", oConexion);
                    cmd.Parameters.AddWithValue("codproveedor", oProveedor.CodProveedor);
                    cmd.Parameters.AddWithValue("direccion", oProveedor.Direccion);
                    cmd.Parameters.AddWithValue("correo", oProveedor.Correo);
                    cmd.Parameters.AddWithValue("razonsocial", oProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("ruc", oProveedor.Ruc);
                    cmd.Parameters.AddWithValue("estado", oProveedor.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
        public bool EliminarProveedor(int CodProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ProveedorEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codproveedor", CodProveedor);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch(Exception e)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using CapaModelo;
using System.Data;

namespace CapaDatos
{
    public class CD_ViaAdministracion
    {
        public static CD_ViaAdministracion _instancia = null;

        private CD_ViaAdministracion()
        {

        }

        public static CD_ViaAdministracion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ViaAdministracion();
                }
                return _instancia;
            }
        }

        public List<ViaAdministracion> ObtenerVia()
        {
            var rptListaVia = new List<ViaAdministracion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ViaObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaVia.Add(new ViaAdministracion()
                        {
                            CodVia = Convert.ToInt32(dr["CodVia"].ToString()),
                            Via = dr["Via"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaVia;

                }
                catch
                {
                    rptListaVia = null;
                    return rptListaVia;
                }
            }
        }

        public bool RegistrarVia(ViaAdministracion oViaAdministracion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarVia", oConexion);
                    cmd.Parameters.AddWithValue("via", oViaAdministracion.Via);
                    cmd.Parameters.AddWithValue("descripcion", oViaAdministracion.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oViaAdministracion.Estado);
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

        public bool ModificarVia(ViaAdministracion oViaAdministracion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarVia", oConexion);
                    cmd.Parameters.AddWithValue("codVia", oViaAdministracion.CodVia);
                    cmd.Parameters.AddWithValue("via", oViaAdministracion.Via);
                    cmd.Parameters.AddWithValue("descripcion", oViaAdministracion.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oViaAdministracion.Estado);
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

        public bool EliminarVia(int CodVia)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ViaEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codVia", CodVia);
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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Presentacion
    {
        public static CD_Presentacion _instancia = null;

        private CD_Presentacion()
        {

        }
        public static CD_Presentacion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Presentacion();
                }
                return _instancia;
            }
        }
        public List<Presentacion> ObtenerPresentacion()
        {
            var rptListaPresentacion = new List<Presentacion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_PresentacionObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPresentacion.Add(new Presentacion()
                        {
                            CodPresentacion = Convert.ToInt32(dr["CodPresentacion"].ToString()),
                            Presentaciones = dr["Presentaciones"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaPresentacion;

                }
                catch
                {
                    rptListaPresentacion = null;
                    return rptListaPresentacion;
                }
            }
        }
        public bool RegistrarPresentacion(Presentacion oPresentacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarPresentacion", oConexion);
                    cmd.Parameters.AddWithValue("codpresentacion", oPresentacion.CodPresentacion);
                    cmd.Parameters.AddWithValue("presentaciones", oPresentacion.Presentaciones);
                    cmd.Parameters.AddWithValue("descripcion", oPresentacion.Descripcion);
                    cmd.Parameters.AddWithValue("estado", oPresentacion.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch(Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public bool ModificarPresentacion(Presentacion oPresentacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarPresentacion", oConexion);
                    cmd.Parameters.AddWithValue("codpresentacion", oPresentacion.CodPresentacion);
                    cmd.Parameters.AddWithValue("presentaciones", oPresentacion.Presentaciones);
                    cmd.Parameters.AddWithValue("descripcion", oPresentacion.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oPresentacion.Estado);
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
        public bool EliminarPresentacion(int CodPresentacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_PresentacionEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codpresentacion", CodPresentacion);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Bodega
    {
        public static CD_Bodega _instancia = null;

        private CD_Bodega()
        {

        }

        public static CD_Bodega Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Bodega();
                }
                return _instancia;
            }
        }

        public List<Bodega> ObtenerBodega()
        {
            var rptListaBodega = new List<Bodega>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_BodegaObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaBodega.Add(new Bodega()
                        {
                            CodBodega = Convert.ToInt32(dr["CodBodega"].ToString()),
                            NombreBodega = dr["NombreBodega"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaBodega;

                }
                catch
                {
                    rptListaBodega = null;
                    return rptListaBodega;
                }
            }
        }

        public bool RegistrarBodega(Bodega oBodega)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[USP_RegistrarBodega]", oConexion);
                    cmd.Parameters.AddWithValue("nombreBodega", oBodega.NombreBodega);
                    cmd.Parameters.AddWithValue("Estado", oBodega.Estado);
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

        public bool ModificarBodega(Bodega oBodega)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarBodega", oConexion);
                    cmd.Parameters.AddWithValue("codBodega", oBodega.CodBodega);
                    cmd.Parameters.AddWithValue("nombreBodega", oBodega.NombreBodega);
                    cmd.Parameters.AddWithValue("Estado", oBodega.Estado);
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
        public bool EliminarBodega(int Codbodega)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_BodegaEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codBodega", Codbodega);
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

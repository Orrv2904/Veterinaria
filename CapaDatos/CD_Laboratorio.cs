using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Laboratorio
    {
        public static CD_Laboratorio _instancia = null;

        private CD_Laboratorio()
        {

        }

        public static CD_Laboratorio Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Laboratorio();
                }
                return _instancia;
            }
        }

        public List<Laboratorios> ObtenerLaboratorio()
        {
            var rptListaLaboratorios = new List<Laboratorios>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_LaboratorioObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaLaboratorios.Add(new Laboratorios()
                        {
                            CodLaboratorio = Convert.ToInt32(dr["CodLaboratorio"].ToString()),
                            Direccion = dr["Direccion"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            RazonSocial = dr["RazonSocial"].ToString(),
                            Ruc = dr["RUC"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaLaboratorios;

                }
                catch
                {
                    rptListaLaboratorios = null;
                    return rptListaLaboratorios;
                }
            }
        }

        public bool RegistrarLaboratorio(Laboratorios oLaboratorio)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarLaboratio", oConexion);
                    cmd.Parameters.AddWithValue("direccion", oLaboratorio.Direccion);
                    cmd.Parameters.AddWithValue("correo", oLaboratorio.Correo);
                    cmd.Parameters.AddWithValue("telefono", oLaboratorio.Telefono);
                    cmd.Parameters.AddWithValue("razonsocial", oLaboratorio.RazonSocial);
                    cmd.Parameters.AddWithValue("ruc", oLaboratorio.Ruc);
                    cmd.Parameters.AddWithValue("Estado", oLaboratorio.Estado);
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

        public bool ModificarLaboratorio(Laboratorios oLaboratorio)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarLaboratorio", oConexion);
                    cmd.Parameters.AddWithValue("codLaboratorio", oLaboratorio.CodLaboratorio);
                    cmd.Parameters.AddWithValue("direccion", oLaboratorio.Direccion);
                    cmd.Parameters.AddWithValue("correo", oLaboratorio.Correo);
                    cmd.Parameters.AddWithValue("telefono", oLaboratorio.Telefono);
                    cmd.Parameters.AddWithValue("razonsocial", oLaboratorio.RazonSocial);
                    cmd.Parameters.AddWithValue("ruc", oLaboratorio.Ruc);
                    cmd.Parameters.AddWithValue("Estado", oLaboratorio.Estado);
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

        public bool EliminarLaboratorio(int CodLaboratorio)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_LaboratorioEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codLaboratorio", CodLaboratorio);
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

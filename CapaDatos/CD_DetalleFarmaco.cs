using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_DetalleFarmaco
    {
        public static CD_DetalleFarmaco _instancia = null;

        private CD_DetalleFarmaco()
        {

        }

        public static CD_DetalleFarmaco Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleFarmaco();
                }
                return _instancia;
            }
        }
        public List<DetalleFarmaco> ObtenerDetalleFarmaco()
        {
            List<DetalleFarmaco> rptListaDetalleFarmaco = new List<DetalleFarmaco>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_DetalleFarmacoObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaDetalleFarmaco.Add(new DetalleFarmaco()
                        {
                            CodDetalleFarmaco = Convert.ToInt32(dr["CodDetalleFarmaco"].ToString()),
                            Concentracion = dr["Concentracion"].ToString(),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()),
                            CodLaboratorio = Convert.ToInt32(dr["CodLaboratorio"].ToString()),
                            Objlaboratorio = new Laboratorios() { RazonSocial = dr["RazonSocial"].ToString() },
                            CodProveedor = Convert.ToInt32(dr["CodProveedor"].ToString()),
                            Objproveedor = new Proveedores() { RazonSocial = dr["RazonSocial"].ToString() },
                            CodVia = Convert.ToInt32(dr["CodVia"].ToString()),
                            ObjviaAdministracion = new ViaAdministracion() { Via = dr["Via"].ToString() },
                            NombreComercial = dr["NombreComercial"].ToString(),
                            NumeroLote = dr["NumeroLote"].ToString()
                        });
                    }
                    dr.Close();

                    return rptListaDetalleFarmaco;

                }
                catch (Exception ex)
                {
                    rptListaDetalleFarmaco = null;
                    return rptListaDetalleFarmaco;
                }
            }
        }

        public bool RegistrarDetalleFarmaco(DetalleFarmaco oDetalle)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarDetalleFarmaco", oConexion);
                    cmd.Parameters.AddWithValue("concentracion", oDetalle.Concentracion);
                    cmd.Parameters.AddWithValue("fechaRegistro", oDetalle.FechaRegistro);
                    cmd.Parameters.AddWithValue("codLaboratorio", oDetalle.CodLaboratorio);
                    cmd.Parameters.AddWithValue("codProveedor", oDetalle.CodProveedor);
                    cmd.Parameters.AddWithValue("codVia", oDetalle.CodVia);
                    cmd.Parameters.AddWithValue("nombreComercial", oDetalle.NombreComercial);
                    cmd.Parameters.AddWithValue("numeroLote", oDetalle.NumeroLote);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ModificarDetalleFarmaco(DetalleFarmaco oDetalleFarmaco)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarDetalleFarmaco", oConexion);
                    cmd.Parameters.AddWithValue("coddetallefarmaco", oDetalleFarmaco.CodDetalleFarmaco);
                    cmd.Parameters.AddWithValue("concentracion", oDetalleFarmaco.Concentracion);
                    cmd.Parameters.AddWithValue("fechaRegistro", oDetalleFarmaco.FechaRegistro);
                    cmd.Parameters.AddWithValue("codlaboratorio", oDetalleFarmaco.CodLaboratorio);
                    cmd.Parameters.AddWithValue("codproveedor", oDetalleFarmaco.CodProveedor);
                    cmd.Parameters.AddWithValue("codvia", oDetalleFarmaco.CodVia);
                    cmd.Parameters.AddWithValue("nombrecomercial", oDetalleFarmaco.NombreComercial);
                    cmd.Parameters.AddWithValue("numerolote", oDetalleFarmaco.NumeroLote);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool EliminarDetalleFarmaco(int IdDetalleFarmaco)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_DetalleFarmacoEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codDetalleFarmaco", IdDetalleFarmaco);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}

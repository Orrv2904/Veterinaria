using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Producto
    {
        public static CD_Producto _instancia = null;

        private CD_Producto()
        {

        }

        public static CD_Producto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Producto();
                }
                return _instancia;
            }
        }

        public List<Productos> ObtenerProducto()
        {
            List<Productos> rptListaProducto = new List<Productos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ProductoObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProducto.Add(new Productos()
                        {
                            Id_Farmaco = Convert.ToInt32(dr["CodProducto"].ToString()),
                            Nombre_Generico = dr["NombreGenerico"].ToString(),
                            Id_Categoria = Convert.ToInt32(dr["CodCategoria"].ToString()),
                            Objcategoria = new Categorias() { Categoria = dr["Categorias"].ToString() },
                            Estado = Convert.ToBoolean(dr["Estado"].ToString()),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();

                    return rptListaProducto;

                }
                catch (Exception e)
                {
                    rptListaProducto = null;
                    return rptListaProducto;
                }
            }
        }

        public bool RegistrarProducto(Productos oProducto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarProducto", oConexion);
                    cmd.Parameters.AddWithValue("categoria", oProducto.Id_Categoria);
                    cmd.Parameters.AddWithValue("descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("nombreGenerico", oProducto.Nombre_Generico);
                    cmd.Parameters.AddWithValue("estado", oProducto.Estado);
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

        public bool ModificarProducto(Productos oProducto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarProducto", oConexion);
                    cmd.Parameters.AddWithValue("codproducto", oProducto.Id_Farmaco);
                    cmd.Parameters.AddWithValue("categoria", oProducto.Id_Categoria);
                    cmd.Parameters.AddWithValue("nombreGenerico", oProducto.Nombre_Generico);
                    cmd.Parameters.AddWithValue("descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("estado", oProducto.Estado);
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

        public bool EliminarProducto(int IdFarmaco)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ProductoEliminar", oConexion);
                    cmd.Parameters.AddWithValue("codproducto", IdFarmaco);
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

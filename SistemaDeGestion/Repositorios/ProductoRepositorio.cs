using SistemaDeGestion.Modelos;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace SistemaDeGestion.Repositorios
{
    public class ProductoRepositorio
    {
        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public ProductoRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public List<Producto> listarproductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", conexion))
                {
                    conexion.Open();
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Producto producto = new Producto();
                                    producto.Id = Convert.ToInt64(reader["Id"]);
                                    producto.Descripcion = reader["Descripciones"].ToString();
                                    producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"].ToString());
                                    producto.PrecioCompra = Convert.ToDouble(reader["Costo"].ToString());
                                    producto.Stock = int.Parse(reader["Stock"].ToString());
                                    producto.IdUsuario = Convert.ToInt64(reader["IdUsuario"]);
                                    listaProductos.Add(producto);
                                }
                            }

                        }
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listaProductos;

        }
    }
}


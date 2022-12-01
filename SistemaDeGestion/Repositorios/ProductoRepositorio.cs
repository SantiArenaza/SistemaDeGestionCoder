using SistemaDeGestion.Modelos;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace SistemaDeGestion.Repositorios
{
    public class ProductoRepositorio
    {
        //defino la cadena de conexion
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
            List<Producto> listaProductos = new List<Producto>(); //lista de producos
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", conexion)) //comando de consulta sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read()) //en cada ciclo del while voy guardando en listaProductos los productos
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
                    conexion.Close(); //cierro conexion
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listaProductos; //retorno la lista

        }
    }
}


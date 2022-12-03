using SistemaDeGestion.Modelos;
using System.Data;
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

        public void crearProducto(Producto producto)  //funcion para crear nuevo producto en la base de datos
        {
            
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripcion, @costo, @precioVenta, @stock, @idUsuario)", conexion)) //comando de consulta sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = producto.PrecioCompra });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    cmd.ExecuteNonQuery();

                    conexion.Close(); //cierro conexion
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool eliminarProducto(long id)  //funcion para eliminar producto en la base de datos por id
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                int filasAfectadas;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM producto WHERE id=@id", conexion)) //comando delete sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    filasAfectadas=cmd.ExecuteNonQuery();
                    conexion.Close(); //cierro conexion
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private Producto obtenerProductoDesdeReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = Convert.ToInt64(reader["Id"]);
            producto.Descripcion = reader["Descripciones"].ToString();
            producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"].ToString());
            producto.PrecioCompra = Convert.ToDouble(reader["Costo"].ToString());
            producto.Stock = int.Parse(reader["Stock"].ToString());
            producto.IdUsuario = Convert.ToInt64(reader["IdUsuario"]);
            return producto;
  
        }

        public Producto? obtenerProducto(long id)  //obtengo producto por id
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE id=@id", conexion)) //comando de consulta sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                      if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoDesdeReader(reader);
                            return producto;
                        }
                      else
                        {
                            return null;
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close(); //cierro conexion
            }

        }

        public Producto? actualizarProducto (long id, Producto productoAAactualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                Producto? producto = obtenerProducto(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> camposAActulizar  = new List<string>();
                if (producto.Descripcion != productoAAactualizar.Descripcion && !string.IsNullOrEmpty(productoAAactualizar.Descripcion))
                {
                    camposAActulizar.Add("Descripciones = @descripcion");
                }
                if (producto.PrecioCompra != productoAAactualizar.PrecioCompra && productoAAactualizar.PrecioCompra>0)
                {
                    camposAActulizar.Add("Costo = @costo");
                }
                if (producto.PrecioVenta != productoAAactualizar.PrecioVenta && productoAAactualizar.PrecioVenta>0)
                {
                    camposAActulizar.Add("PrecioVenta = @precioVenta");
                }
                if (producto.Stock != productoAAactualizar.Stock && productoAAactualizar.Stock > 0)
                {
                    camposAActulizar.Add("Stock = @stock");
                }
                if (producto.IdUsuario != productoAAactualizar.IdUsuario && productoAAactualizar.IdUsuario > 0)
                {
                    camposAActulizar.Add("IdUsuario = @idUsuario");
                }
                if (camposAActulizar.Count == 0)
                {
                    throw new Exception("No hay nada para actualizar");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActulizar)} WHERE id=@id", conexion))
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = productoAAactualizar.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = productoAAactualizar.PrecioCompra });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = productoAAactualizar.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoAAactualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = productoAAactualizar.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return productoAAactualizar;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}


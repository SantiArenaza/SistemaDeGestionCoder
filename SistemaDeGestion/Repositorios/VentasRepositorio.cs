using SistemaDeGestion.Modelos;
using System.Data.SqlClient;

namespace SistemaDeGestion.Repositorios
{
    public class VentasRepositorio
    {
        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public VentasRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public List<Ventas> listarVentas()
        {
            List<Ventas> listaVentas = new List<Ventas>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Ventas ventas = new Ventas();
                                    ventas.Id = Convert.ToInt64(reader["Id"]);
                                    ventas.Comentarios = reader["Comentarios"].ToString();
                                    ventas.IdUsuario = Convert.ToInt64(reader["Idusuario"].ToString());
                                    listaVentas.Add(ventas);
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
            return listaVentas;

        }
    }
}

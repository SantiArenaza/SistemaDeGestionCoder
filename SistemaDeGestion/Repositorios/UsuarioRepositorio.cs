using SistemaDeGestion.Modelos;
using System.Data.SqlClient;

namespace SistemaDeGestion.Repositorios
{
    public class UsuarioRepositorio
    {

        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public UsuarioRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public List<Usuario> listarusuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Usuario usuario = new Usuario();
                                    usuario.Id = Convert.ToInt64(reader["Id"]);
                                    usuario.Nombre = reader["Nombre"].ToString();
                                    usuario.Apellido = reader["Apellido"].ToString();
                                    usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                    usuario.Contraseña = reader["Contraseña"].ToString();
                                    usuario.Mail = reader["Mail"].ToString();
                                    listaUsuarios.Add(usuario);
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
            return listaUsuarios;

        }

    }
}

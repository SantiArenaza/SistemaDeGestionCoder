using SistemaDeGestion.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeGestion.Repositorios
{
    public class LoginRepositorio
    {
        //defino la cadena de conexion
        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public LoginRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public bool verificarUsuario(Usuario usuario)  
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario=@nombreUsuario AND Contraseña=@contrasenia", conexion)) //comando de consulta sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;   
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

    }
}

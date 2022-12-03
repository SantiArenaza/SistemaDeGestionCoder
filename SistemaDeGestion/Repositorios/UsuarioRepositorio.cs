using SistemaDeGestion.Modelos;
using System.Data;
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

        /*actualizar usuario*/

        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt64(reader["Id"]);
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contraseña = reader["Contraseña"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;

        }

        public Usuario? obtenerUsuario(long id)  //obtengo usuario por id
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE id=@id", conexion)) //comando de consulta sql
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioDesdeReader(reader);
                            return usuario;
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

        public Usuario? actualizarUsuario(long id, Usuario usuarioAAactualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActulizar = new List<string>();
                if (usuario.Nombre != usuarioAAactualizar.Nombre && !string.IsNullOrEmpty(usuarioAAactualizar.Nombre))
                {
                    camposAActulizar.Add("Nombre = @nombre");
                }
                if (usuario.Apellido != usuarioAAactualizar.Apellido && !string.IsNullOrEmpty(usuarioAAactualizar.Apellido))
                {
                    camposAActulizar.Add("Apellido = @apellido");
                }
                if (usuario.NombreUsuario != usuarioAAactualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAAactualizar.NombreUsuario))
                {
                    camposAActulizar.Add("NombreUsuario = @nombreUsuario");
                }
                if (usuario.Contraseña != usuarioAAactualizar.Contraseña && !string.IsNullOrEmpty(usuarioAAactualizar.Contraseña))
                {
                    camposAActulizar.Add("Contraseña = @contraseña");
                }
                if (usuario.Mail != usuarioAAactualizar.Mail && !string.IsNullOrEmpty(usuarioAAactualizar.Mail))
                {
                    camposAActulizar.Add("Mail = @mail");
                }
                if (camposAActulizar.Count == 0)
                {
                    throw new Exception("No hay campo para actualizar");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActulizar)} WHERE id=@id", conexion))
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioAAactualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioAAactualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioAAactualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuarioAAactualizar.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioAAactualizar.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return usuarioAAactualizar;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}

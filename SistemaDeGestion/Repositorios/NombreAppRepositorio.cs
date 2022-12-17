using SistemaDeGestion.Modelos;
using System.Data.SqlClient;

namespace SistemaDeGestion.Repositorios
{
    public class NombreAppRepositorio
    {
        //defino la cadena de conexion
        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public NombreAppRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public string nombreApp()
        {
            return ("MiApp");
        }



    }
}

namespace SistemaDeGestion.Modelos
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }

        public Usuario()
        {
            Id=0;
            Nombre = "";
            Apellido = "";
            NombreUsuario = "";
            Contraseña = "";
            Mail = "";
            
        }

        public Usuario(long id, string nombre, string apellido, string nombreusuario, string contraseña, string mail)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreusuario;
            Contraseña = contraseña;
            Mail = mail;


        }




    }
}

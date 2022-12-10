namespace SistemaDeGestion.Modelos
{
    public class Ventas
    {
        public long Id { get; set; }
        public string Comentarios { get; set; }
        public long IdUsuario { get; set; }

        public List<ProductosVendidos>? ProductosVendidos { get; set; }

        public Ventas()
        {
            Id = 0;
            Comentarios = "";
            IdUsuario = 0;

        }

        public Ventas(long id, string comenterios, long idusuario)
        {
            Id = id;
            Comentarios = comenterios;
            IdUsuario = idusuario;
        }
    }
}

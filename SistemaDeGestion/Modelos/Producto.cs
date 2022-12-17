using System.Diagnostics.Contracts;

namespace SistemaDeGestion.Modelos
{
    public class Producto
    {
        //Atributos de la clase productos
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public long IdUsuario { get; set; }
        public long IdVenta { get; set; }

        //constructores de la clase producto
        public Producto()
        {
            Id=0;
            Descripcion = "";
            PrecioCompra = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
            IdVenta = -1; //lo uso para cuando traigo solo los productos vendido de manera de tener el id de venta
            
        }

        public Producto(long id, string descripcion, double preciocompra, double precioventa,int stock, long idusuario, long idVenta)
        {
            Id = id;
            Descripcion = descripcion;
            PrecioCompra = preciocompra;
            PrecioVenta = precioventa;
            Stock = stock;
            IdUsuario = idusuario;
            IdVenta = idVenta;
        }



    }
}

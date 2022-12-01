namespace SistemaDeGestion.Modelos
{
    public class ProductosVendidos
    {
        public long Id { get; set; }
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta { get; set; }

        public ProductosVendidos()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
        }

        public ProductosVendidos(long id, int stock, long idproducto, long idventa)
        {
            Id = id;
            Stock = stock;
            IdProducto = idproducto;
            IdVenta = idventa;
        }
    }
}

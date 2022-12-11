using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;
using System.Security.Cryptography.X509Certificates;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosVendidosController : Controller
    {
        private ProductosVendidosRepositorio repositorio = new ProductosVendidosRepositorio();
        private VentasRepositorio repositorioVentas = new VentasRepositorio();
        private ProductoRepositorio repositorioProducto = new ProductoRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta
        {
            try
            {
                List<ProductosVendidos> lista = repositorio.listarProductosVendidos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost] //accion para agregar nueva venta
        public ActionResult Post([FromBody] Ventas venta) //frombody toma la venta desde el cuerpo (lo ingreso desde la API)
        {
            try
            {
                long idVenta = repositorioVentas.agregarVenta(venta); //es un long ya que la funcion agregarVenta me devuelve el id de la venta cargada
                ProductosVendidos productosVendidos = new ProductosVendidos();
                //List<ProductosVendidos>? productoVenta = venta.ProductosVendidos;
                //productosVendidos = productoVenta[0];
                for (var i = 0; i < venta.ProductosVendidos.Count; i++) //recorro la lista de los productos vendidos que se realizaron en la venta 
                {
                    productosVendidos = venta.ProductosVendidos[i]; 
                    productosVendidos.IdVenta = idVenta; //almaceno en el idventa de producto vendido el id de esta venta
                    repositorio.agregarProductoVendido(productosVendidos); //agregrego el producto vendido a partir de la venta realizada
                    repositorioProducto.actualizarStockProducto(productosVendidos.IdProducto, productosVendidos.Stock);  //llamo a la funcion actualizar producto para descontar stock vendido
                }
                return Ok();
               
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

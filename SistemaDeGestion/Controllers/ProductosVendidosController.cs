using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;
using System.Collections.Immutable;
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

        [HttpGet("{idUsuario}")]
        public IActionResult Get(long idUsuario)  //accion de consulta. Consulto los productos que vendio cada usuario a partir de su ID
        {
            try
            {
                List<Ventas> lista = repositorioVentas.listarVentasUsuario(idUsuario); //listo las ventas del usuario
                List<ProductosVendidos> listaProductosVendidos = new List<ProductosVendidos>();
                List<ProductosVendidos> listaProductosVendidosUsuario = new List<ProductosVendidos>();
                Producto? productosVendidos = new Producto();
                List<Producto> listaProductos = new List<Producto>();

                long[] idVenta = new long[lista.Count];
                for (var i = 0; i < lista.Count; i++) //recorro el for para ir alistando los id de los productos que se vendieron en cada venta
                {
                    idVenta[i] = lista[i].Id;
                    listaProductosVendidos = repositorio.listarProductosVendidosUsuario(idVenta[i]);
                    for (var d = 0; d < listaProductosVendidos.Count; d++)
                    {
                        listaProductosVendidosUsuario.Add(listaProductosVendidos[d]);
                    }     
                }

                long[] idProducto = new long[listaProductosVendidosUsuario.Count];
                
                for (var i = 0; i < listaProductosVendidosUsuario.Count; i++) //recorro el for para ir alistando los productos que se vendieron en cada venta a partir de su id
                {
                    idProducto[i] = listaProductosVendidosUsuario[i].IdProducto;
                    productosVendidos = repositorioProducto.obtenerProducto(idProducto[i]);
                    listaProductos.Add(productosVendidos);
                }

                return Ok(listaProductos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

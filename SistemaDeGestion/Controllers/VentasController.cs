using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;
using System.Collections.Immutable;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private VentasRepositorio repositorio = new VentasRepositorio();
        private ProductosVendidosRepositorio repositorioProductoVend = new ProductosVendidosRepositorio();
        private ProductoRepositorio repositorioProducto = new ProductoRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta - traigo ventas y alisto informacion de los productos que se vendieron
        {
            try
            {
                //List<Ventas> lista = repositorio.listarVentas();
                List<ProductosVendidos> lista = repositorioProductoVend.listarProductosVendidos();
                List<Producto>? listaProductosVendidos = new List<Producto>();
                Producto? productosVendidos = new Producto();
                /*
                long[] idVenta = new long[lista.Count];
                for (var i = 0; i < lista.Count; i++)
                {
                    idVenta[i] = lista[i].Id;
                }
                */

                long[] idProducto = new long[lista.Count];
                long[] idVenta = new long[lista.Count];
                for (var i = 0; i < lista.Count; i++) //recorro el for para ir alistando los productos vendidos a partir del id
                {
                    idProducto[i] = lista[i].IdProducto;
                    idVenta[i] = lista[i].IdVenta;
                    productosVendidos = repositorioProducto.obtenerProducto(idProducto[i]);
                    productosVendidos.IdVenta=idVenta[i];   
                    listaProductosVendidos.Add(productosVendidos);
                }
                


                return Ok(listaProductosVendidos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /*[HttpPost] //accion para agregar una venta
        public ActionResult Post([FromBody] Ventas venta) //frombody toma la venta desde el cuerpo (lo ingreso desde la API)
        {
            try
            {
                repositorio.agregarVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }*/


        [HttpDelete]
        public ActionResult Delete([FromBody] long id) //accion para borrar una venta desde el id
        {
            try
            {
                bool seElimino = repositorio.eliminarVenta(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound(); //arroja error 404 si no se encuentra registro, osea id a eliminar en este caso
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

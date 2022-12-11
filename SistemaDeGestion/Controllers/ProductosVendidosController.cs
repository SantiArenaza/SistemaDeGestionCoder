using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosVendidosController : Controller
    {
        private ProductosVendidosRepositorio repositorio = new ProductosVendidosRepositorio();
        private VentasRepositorio repositorioVentas = new VentasRepositorio();

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
                repositorioVentas.agregarVenta(venta);
                repositorio.agregarProductoVendido(venta.ProductosVendidos);
                return Ok();
               
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoRepositorio repositorio = new ProductoRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta
        {
            try
            {
                List<Producto> lista = repositorio.listarproductos(); //llamo a la funcion listarproductos dentro de la clase producto repositorio y guardo la lista
                return Ok(lista); //retorno la lista
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost] //accion para agregar un producto
        public ActionResult Post([FromBody] Producto producto) //frombody toma el producto desde el cuerpo (lo ingreso desde la API)
        {
            try
            {
                repositorio.crearProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] long id) //accion para borrar un producto 
        {
            try
            {
                bool seElimino = repositorio.eliminarProducto(id);
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

        [HttpPut]
        public ActionResult<Producto> Put(long id, [FromBody] Producto productoAActualizar)
        {
            try
            {
              Producto? productoActualizado = repositorio.actualizarProducto(id, productoAActualizar);
              if (productoActualizado != null)
                {
                    return Ok(productoActualizado);
                }
              else
                {
                    return NotFound("El producto no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}

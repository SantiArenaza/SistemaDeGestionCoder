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
    }
}

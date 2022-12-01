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
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private VentasRepositorio repositorio = new VentasRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta
        {
            try
            {
                List<Ventas> lista = repositorio.listarVentas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

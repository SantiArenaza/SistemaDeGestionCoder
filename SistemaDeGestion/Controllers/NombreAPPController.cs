using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreAPPController : Controller
    {

        private NombreAppRepositorio repositorio = new NombreAppRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta
        {
            try
            {
                string nombreApp = repositorio.nombreApp(); //llamo a la funcion listarproductos dentro de la clase producto repositorio y guardo la lista
                return Ok(nombreApp); //retorno la lista
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

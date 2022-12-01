using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepositorio repositorio = new UsuarioRepositorio();

        [HttpGet]
        public IActionResult Get()  //accion de consulta
        {
            try
            {
                List<Usuario> lista = repositorio.listarusuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaDeGestion.Modelos;
using SistemaDeGestion.Repositorios;

namespace SistemaDeGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private LoginRepositorio repositorio = new LoginRepositorio();

        [HttpPost] 
        public ActionResult <Usuario> Login (Usuario usuario)
        {
            try
            {
                bool usuarioExiste = repositorio.verificarUsuario(usuario);
                if (usuarioExiste)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

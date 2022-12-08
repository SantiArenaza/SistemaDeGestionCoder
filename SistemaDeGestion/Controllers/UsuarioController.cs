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

        [HttpPut]  
        public ActionResult<Usuario> Put(long id, [FromBody] Usuario usuarioAActualizar)  //accion de actualizar usuario
        {
            try
            {
                Usuario? usuarioActualizado = repositorio.actualizarUsuario(id, usuarioAActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        /*
        [HttpGet("{id}")]
        public IActionResult Get(long id)  //accion para traer usuario mediante su id
        {
            try
            {
                Usuario? traerUsuario = repositorio.obtenerUsuario(id);
                if (traerUsuario != null)
                {
                    return Ok(traerUsuario);
                }
                else
                {
                    return NotFound("El usuario no existe");
                }
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        } */

        [HttpGet("{nombreUsuario}")]
        public IActionResult Get(string nombreUsuario)  //accion para traer usuario mediante nombre de usuario
        {
            try
            {
                Usuario? traerUsuario = repositorio.obtenerUsuarioPorNombreUsuario(nombreUsuario);
                if (traerUsuario != null)
                {
                    return Ok(traerUsuario);
                }
                else
                {
                    return NotFound("El usuario no existe");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] long id) //accion para borrar un usaurio
        {
            try
            {
                bool seElimino = repositorio.eliminarUsuario(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("El usuario que se intenta eliminar no existe"); //arroja error 404 si no se encuentra registro, osea id a eliminar en este caso
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost] //accion para agregar un usuario nuevo
        public ActionResult Post([FromBody] Usuario usuario) //frombody toma el producto desde el cuerpo (lo ingreso desde la API)
        {
            try
            {

                if (usuario.Nombre.Length==0 | usuario.Apellido.Length == 0 | usuario.NombreUsuario.Length == 0 | usuario.Contraseña.Length == 0 | usuario.Mail.Length == 0)
                {
                    return NotFound("No se ingresaron todos los campos necesarios para crear un usuario");
                }
                else
                {
                    repositorio.crearUsuario(usuario);
                    if (usuario.NombreUsuario== "UsuarioExistente")
                    {
                        return NotFound("Ya existe un usuario con el nombre de usuario ingresado");
                    }
                    else
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}

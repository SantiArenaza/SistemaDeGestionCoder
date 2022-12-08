﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost] //accion para agregar una venta
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
        }


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

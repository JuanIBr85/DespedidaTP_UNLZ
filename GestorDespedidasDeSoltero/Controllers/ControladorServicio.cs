using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ControladorServicio : Controller
    {

        private IServicioDeServicios sServicio;

        public ControladorServicio(IServicioDeServicios _sServicio)
        {
            sServicio = _sServicio;
        }

        [HttpGet("ObtenerServicios")]
        public IActionResult Servicios()
        {
            return Ok(sServicio.ObtenerServicios());
        }

        [HttpGet("ObtenerServicio/{IdServicio:int}")]
        public IActionResult ObtenerServicioId(int IdServicio)
        {
            Servicio servicios = sServicio.ObtenerServicioId(IdServicio);

            if (servicios == null)
                return NotFound();
            else
                return Ok(servicios);

        }

        [HttpPost("NuevoServicio")]
        public IActionResult NuevoServicio([FromBody] Servicio servicios)
        {
            sServicio.AgregarServicio(servicios);

            return Ok();
        }

        [HttpPut("ModificarServicio/{IdServicio:int}")]
        public IActionResult ModificarServicio(int IdServicio, [FromBody] Servicio servicios)
        {
            sServicio.ModificarServicio(IdServicio,servicios);
            return Ok();
        }

        [HttpPatch("BorradoLogicoServicio/{IdServicio:int}")]
        public IActionResult BorradoLogicoSerivicio(int IdServicio)
        {
            sServicio.BorradoLogicoServicio(IdServicio);
            return Ok(); 
        }
        /*
        [HttpDelete("BorrardoFisicoServicio/{IdServicio:int}")]
        public IActionResult BorrarServicio(int IdServicio)
        {
            sServicio.BorradoFisicoServicio(IdServicio);
            return Ok();
        }*/
        
    }
}

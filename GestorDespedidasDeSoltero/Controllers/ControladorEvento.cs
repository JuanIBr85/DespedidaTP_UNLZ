using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorEvento : Controller
    {

        private IServicioEventos sEventos;

        public ControladorEvento(IServicioEventos _sEventos)
        {
            sEventos = _sEventos;
        }

        [HttpGet("ObtenerEventos")]
        public IActionResult ObtenerEventos()
        {
            return Ok(sEventos.ObtenerEventos());
        }

        [HttpGet("ObtenerEventosId/{IdEvento:int}")]
        public IActionResult ObtenerEventosId(int IdEvento)
        {
            Evento evento = sEventos.ObtenerEventosId(IdEvento);

            if (evento == null)
                return NotFound();
            else
                return Ok(evento);
        }

        [HttpPost("AgregarEvento")]
        public IActionResult AgregarEvento([FromBody] Evento evento)
        {
            sEventos.AgregarEvento(evento);
            return Ok();
        }

        [HttpPut("ModificarEvento/{IdEvento:int}")]
        public IActionResult ModificarEvento(int IdEvento, [FromBody] Evento evento)
        {
            sEventos.ModificarEvento(IdEvento, evento);
            return Ok();
        }

        [HttpPatch("BorradoLogicoEvento/{IdEvento:int}")]
        public IActionResult BorradoLogicoEvento(int IdEvento)
        {
            sEventos.BorradoLogicoEvento(IdEvento);
            return Ok();
        }
        /*
        [HttpDelete("BorradoFisicoEvento/{IdEvento:int}")]
        public IActionResult BorradoFisicoServicios(int IdEvento)
        {
            sEventos.BorradoFisicoEvento(IdEvento);
            return Ok();
        }*/

    }
}

using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoEventoControlador : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {

            ServicioTiposDeEventos sEventos = new ServicioTiposDeEventos();

            return Ok(sEventos.GetTipoEventos());
            
        }

        [HttpGet("{IdEvento:int}")]
        public IActionResult GetTipoEventosId(int IdEvento) 
        {

            ServicioTiposDeEventos sEventos = new ServicioTiposDeEventos();
            TipoEvento servicio = sEventos.GetTipoEventoId(IdEvento);

            if (servicio == null)
                return NotFound();
            else
                return Ok(sEventos.GetTipoEventoId(IdEvento));


        }

    }
}

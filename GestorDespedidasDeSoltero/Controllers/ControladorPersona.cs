 using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorPersona : Controller
    {

        private IServicioPersonas sPersonas;

        public ControladorPersona(IServicioPersonas _sPersonas)
        {
            sPersonas = _sPersonas;
        }

        [HttpGet("ObtenerPersonas")]
        public IActionResult Get()
        {
            return Ok(sPersonas.ObtenerPersonas());
        }

        [HttpGet("ObtenerPersona/{IdPersona:int}")]

        public IActionResult GetId(int IdPersona)
        {
            Personas personas = sPersonas.ObtenerIdPersona(IdPersona);

            if (personas == null)
                return NotFound(); 
            else 
                return Ok(personas);

        }

        [HttpPost("AgregarNuevaPersona")]
        public IActionResult AgregarPersona([FromBody] Personas personas)
        {
            sPersonas.AgregarPersona(personas);
            return Ok(new {exito = true, mensaje = "Persona creada"});
        }

        [HttpPut("ModificarPersona/{IdPersona:int}")]
        public IActionResult ModificarPersona(int IdPersona, Personas personas)
        {
            sPersonas.ModificarPersona(IdPersona, personas);
            return Ok();
        }
        
        [HttpPatch("BorradoLogicoPersona/{IdPersona:int}")]
        public IActionResult BorradoLogicoPersona(int IdPersona)
        {
            sPersonas.BorradoLogicoPersona(IdPersona);
            return Ok();
        }

        [HttpPatch("DesacerBorradoLogicoPersona/{IdPersona:int}")]
        public IActionResult DesacerBorradoLogicoPersona(int IdPersona)
        {
            sPersonas.DesacerBorradoLogicoPersona(IdPersona);
            return Ok();
        }
        /*
        [HttpDelete("BorradoFisicoPersona/{IdPersona:int}")]
        public IActionResult BorradoFisicoPersona(int IdPersona)
        {
            sPersonas.BorradoFisicoPersona(IdPersona);
            return Ok();
        }*/
    }
}

using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ControladorLocalidad : Controller
    {
        private IServicioLocalidad sLocalidad;

        public ControladorLocalidad(IServicioLocalidad _sLocalidad)
        {
            sLocalidad = _sLocalidad;
        }

        [HttpGet("ObtenerLocalidad")]
        public IActionResult ObtenerLocalidad()
        {
            return Ok(sLocalidad.ObtenerLocalidad());
        }

        [HttpGet("ObtenerLocalidad/{IdLocalidad:int}")]
        public IActionResult ObtenerProvinciaId(int IdLocalidad)
        {
            Localidad localidad = sLocalidad.ObtenerLocalidadId(IdLocalidad);

            if (localidad == null)
                return NotFound();
            else
                return Ok(localidad);

        }

        [HttpPost("AgregarLocalidad")]
        public IActionResult AgregarLocalidad([FromBody] Localidad localidad)
        {
            sLocalidad.AgregarLocalidad(localidad);
            return Ok();
        }

        [HttpPut("ModificarLocalidad/{IdLocalidad:int}")]
        public IActionResult ModificarLocalidad(int IdLocalidad, [FromBody] Localidad localidad)
        {
            sLocalidad.ModificarLocalidad(IdLocalidad, localidad);
            return Ok();
        }

        [HttpPatch("BorradoLogicoLocalidad/{IdLocalidad:int}")]
        public IActionResult BorradoLogicoLocalidad(int IdLocalidad)
        {
            sLocalidad.BorradoLogicoLocalidad(IdLocalidad);
            return Ok();
        }
        /*
        [HttpDelete("BorrardoFisicoLocalidad/{IdLocalidad:int}")]
        public IActionResult BorrardoFisicoLocalidad(int IdLocalidad)
        {
            sLocalidad.BorradoFisicoLocalidad(IdLocalidad);
            return Ok();
        }*/
    }
}

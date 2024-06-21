using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorProvincia : Controller
    {
        private IServicioProvincia sProvincia;

        public ControladorProvincia(IServicioProvincia _sProvincia)
        {
            sProvincia = _sProvincia;
        }

        [HttpGet("ObtenerProvincia")]
        public IActionResult ObtenerProvincia()
        {
            return Ok(sProvincia.ObtenerProvincia());
        }

        [HttpGet("ObtenerProvincia/{IdProvincia:int}")]
        public IActionResult ObtenerProvinciaId(int IdProvincia)
        {
            Provincia provincia = sProvincia.ObtenerProvinciaId(IdProvincia);

            if (provincia == null)
                return NotFound();
            else
                return Ok(provincia);

        }
        /*
        [HttpPost("AgregarProvincia")]
        public IActionResult AgregarProvincia([FromBody] Provincia provincia)
        {
            sProvincia.AgregarProvincia(provincia);
            return Ok();
        }

        [HttpPut("ModificarProvincia/{IdProvincia:int}")]
        public IActionResult ModificarProvincia(int IdProvincia, [FromBody] Provincia provincia)
        {
            sProvincia.ModificarProvincia(IdProvincia, provincia);
            return Ok();
        }

        [HttpPatch("BorradoLogicoProvincia/{IdProvincia:int}")]
        public IActionResult BorradoLogicoSerivicio(int IdProvincia)
        {
            sProvincia.BorradoLogicoProvincia(IdProvincia);
            return Ok();
        }

        [HttpDelete("BorrardoFisicoProvincia/{IdProvincia:int}")]
        public IActionResult BorrarServicio(int IdProvincia)
        {
            sProvincia.BorradoFisicoProvincia(IdProvincia);
            return Ok();
        }*/

    }
}

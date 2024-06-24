using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorDespedidasDeSoltero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorUsuario : Controller
    {
        private IServicioUsuarios sUsuarios;

        public ControladorUsuario(IServicioUsuarios _sUsuarios)
        {
            sUsuarios = _sUsuarios;
        }

    }
}

﻿using GestorEventos.Servicios.Entidades;
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

        /*[HttpPost("ValidarUsuario")]
        public IActionResult ValidarUsuario([FromBody] Usuario usuario)
        {
            string resultado = sUsuarios.ValidarUsuario(usuario.nombre_usuario, usuario.clave_usuario);

            return Ok(resultado);
          
        }*/

        /*[HttpPost("RegistrarUsuario")]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            bool resultado = sUsuarios.RegistrarUsuario(usuario);

            return Ok(resultado);
            
        }*/

    }
}

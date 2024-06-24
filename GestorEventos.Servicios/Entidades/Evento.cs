using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Entidades
{
    public class Evento
    {

        public int IdEvento { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdPersonaAgasajada { get; set; }
        public int IdUsuario { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public int CantidadPersonas { get; set; }
        public int IdEstadoEvento {  get; set; }
        public bool Borrado { get; set; }

    }

    public class EventoModel : Evento //Solo admite herencia simple, no podemos hacer heredar tambien desde Persona, por eso agrego los datos de la clase persona
    {
        //public Evento Evento { get; set; }

        //public IEnumerable<EventosServicios> ListaDeServiciosContratados {  get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DireccionCalle { get; set; }
        public string DireccionNumero { get; set; }
        public string DireccionPiso { get; set; }
        public string DireccionDepartamento { get; set; }
        public string DireccionCodPostal { get; set; }
        public bool Borrado { get; set; }
        public IEnumerable<EventosServicios> ListaDeServiciosContratados { get; set; }
        public IEnumerable<Servicio> ListaDeServiciosDisponibles { get; set; }

    }

    public class EventoViewModel : Evento // hereda de evento
    {
        public string EstadoEvento { get; set; }
        public string NombrePersonaAgasajada { get; set; }
        public string TipoEvento { get; set; }
        public string NombreUsuario { get; set; }
    }

}

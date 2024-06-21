using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Entidades
{
    public class EventosServicios
    {
        public int IdEventoServicio {  get; set; }
        public int IdEvento {  get; set; }
        public int IdServicio { get; set; }
        public bool Borrado { get; set; }
    }
}

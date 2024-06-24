using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Drawing.Printing;
using System.Globalization;

namespace GestosEventos.WebUsuario.Controllers
{
    public class EventosController : Controller
    {
        private IServicioEventos eventoService;
        private IServicioPersonas personaService;
        private IServicioDeServicios servicioService;
        private IEventosServiciosService eventosServiciosService;

        public EventosController(IServicioEventos _eventoService, IServicioPersonas _personaService, IServicioDeServicios _servicioService, IEventosServiciosService _eventosServiciosService)
        {
           this.eventoService = _eventoService;
            this.personaService = _personaService;
            this.servicioService = _servicioService;
           this.eventosServiciosService = _eventosServiciosService;
        }
        // GET: EventosController
        public ActionResult Index()
        {
            int IdUsuario = int.Parse(
             HttpContext.User.Claims.First(x => x.Type == "usuarioSolterout").Value);

            ServicioEventos servicioEventos = new ServicioEventos();
            //servicioEventos.ObtenerEventos();
            
            return View(servicioEventos.ObtenerMisEventos(IdUsuario));
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
            int idUsuario;
            try
            {
                idUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "usuarioSolterout").Value);
            }
            catch (Exception ex)
            {
                // Manejar el error adecuadamente
                return BadRequest("Usuario no válido.");
            }

            var evento = this.eventoService.ObtenerMisEventos(idUsuario).FirstOrDefault(x => x.IdEvento == id);
            if (evento == null)
            {
                // Manejar el caso donde el evento no se encuentra
                return NotFound("Evento no encontrado.");
            }

            var listaServiciosDisponibles = this.servicioService.ObtenerServicios();
            var listaIdServiciosContratados = this.eventosServiciosService.GetServiciosPorEvento(evento.IdEvento);
            List<Servicio> listaServicios = new List<Servicio>();

            foreach (var servicio in listaIdServiciosContratados)
            {
                var servicioContratado = listaServiciosDisponibles.FirstOrDefault(x => x.IdServicio == servicio.IdServicio);
                if (servicioContratado != null)
                {
                    listaServicios.Add(servicioContratado);
                }
                else
                {
                    // Manejar el caso donde el servicio contratado no se encuentra en la lista de servicios disponibles
                    // Podrías registrar un mensaje de error o manejarlo según sea necesario
                }
            }


            ViewData["ListaServicios"] = listaServicios;

            return View(evento);
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            var evento = new EventoModel();
            evento.ListaDeServiciosDisponibles = this.servicioService.ObtenerServicios();

            return View(evento);

        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
          
                Personas personaAgasajada = new Personas();
                personaAgasajada.Nombre = collection["Nombre"].ToString();
                personaAgasajada.Apellido = collection["Apellido"].ToString();
                personaAgasajada.Email = collection["Email"].ToString();
                personaAgasajada.Telefono= collection["Telefono"].ToString();
                personaAgasajada.DireccionCalle = collection["DireccionCalle"].ToString();
                personaAgasajada.DireccionNumero = collection["DireccionNumero"].ToString();
                personaAgasajada.DireccionPiso = collection["DireccionPiso"].ToString();
                personaAgasajada.DireccionDepartamento = collection["DireccionDepartamento"].ToString();
                personaAgasajada.DireccionCodPostal = collection["DireccionCodPostal"].ToString();
                personaAgasajada.Borrado = false;

                int IdPersonaAgasajada = personaService.AgregarPersona(personaAgasajada);

                Evento eventoNuevo = new Evento();
                eventoNuevo.IdPersonaAgasajada = IdPersonaAgasajada;
                //eventoNuevo.CantidadPersonas = int.Parse(collection["CantidadPersonas"].ToString());
                if (collection.ContainsKey("CantidadPersonas") && int.TryParse(collection["CantidadPersonas"].ToString(), out int cantidadPersonas))
                {
                    eventoNuevo.CantidadPersonas = cantidadPersonas;
                }
                else
                {
                    // Manejar error o asignar un valor predeterminado
                    throw new ArgumentException("CantidadPersonas es nulo o no válido.");
                }
                eventoNuevo.IdUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "usuarioSolterout").Value); // HTTPContext.User.Identity.Id reemplaza a IdPersonaAgasajada en entidad evento.
                /*var userId = HttpContext.User?.Identity?.GetUserId<int>();
                if (userId != null)
                {
                    eventoNuevo.IdUsuario = userId.Value;
                }
                else
                {
                    // Manejar error o asignar un valor predeterminado
                    throw new ArgumentException("IdUsuario es nulo o no válido.");
                }*/
                //eventoNuevo.FechaEvento = DateTime.Parse(collection["FechaEvento"].ToString());
                if (collection.ContainsKey("FechaEvento") && DateTime.TryParse(collection["FechaEvento"].ToString(), out DateTime fechaEvento))
                {
                    eventoNuevo.FechaEvento = fechaEvento;
                }
                else
                {
                    // Manejar error o asignar un valor predeterminado
                    throw new ArgumentException("FechaEvento es nulo o no válido.");
                }
                //eventoNuevo.IdTipoEvento= int.Parse(collection["IdTipoEvento"].ToString());
                if (collection.ContainsKey("IdTipoEvento") && int.TryParse(collection["IdTipoEvento"].ToString(), out int idTipoEvento))
                {
                    eventoNuevo.IdTipoEvento = idTipoEvento;
                }
                else
                {
                    // Manejar error o asignar un valor predeterminado
                    throw new ArgumentException("IdTipoEvento es nulo o no válido.");
                }
                //eventoNuevo.NombreEvento = collection["NombreEvento"].ToString();
                if (collection.ContainsKey("NombreEvento"))
                {
                    eventoNuevo.NombreEvento = collection["NombreEvento"].ToString();
                }
                else
                {
                    // Manejar error o asignar un valor predeterminado
                    throw new ArgumentException("NombreEvento es nulo o no válido.");
                }
                
                eventoNuevo.IdEstadoEvento = 1; // Pendiente de aprobacion.


                 int IdEventoNuevo = this.eventoService.AgregarEvento(eventoNuevo);

                // Verificar si collection["ServiciosVM"] tiene elementos
                var serviciosVM = collection["ServiciosVM"];
                if (serviciosVM.Any())
                {
                    foreach (var idServicio in serviciosVM)
                    {
                        EventosServicios relacionEventoServicio = new EventosServicios();
                        relacionEventoServicio.IdEvento = IdEventoNuevo;
                        relacionEventoServicio.IdServicio = int.Parse(idServicio.ToString());
                        relacionEventoServicio.Borrado = false;

                        this.eventosServiciosService.PostNuevoEventoServicio(relacionEventoServicio);
                    }
                }
                else
                {
                    // Mensaje de depuración
                    System.Diagnostics.Debug.WriteLine("ServiciosVM está vacío o no contiene elementos.");
                }



                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // Manejar excepciones de forma apropiada
                // Ejemplo: Registrar el error y/o informar al usuario
                Console.WriteLine($"Error: {ex.Message}");
                return View();
            }
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

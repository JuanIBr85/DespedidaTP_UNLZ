using Dapper;
using GestorEventos.Servicios.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public interface IServicioEventos
    {
        int AgregarEvento(Evento evento);
       // bool BorradoFisicoEvento(int IdEvento);
        bool BorradoLogicoEvento(int IdEvento);
        bool ModificarEvento(int IdEvento, Evento evento);
        IEnumerable<Evento> ObtenerEventos();
        IEnumerable<EventoViewModel> ObtenerEventosViewModel();
        public IEnumerable<EventoViewModel> ObtenerMisEventos(int IdUsuario);
        Evento ObtenerEventosId(int IdEvento);

        //bool PutNuevoEvento(int idEvento, Eventos eventos);
        bool CambiarEstadoEvento(int IdEvento, int IdEstado);
    }

    public class ServicioEventos : IServicioEventos
    {

        private string _connectionString;

        public ServicioEventos()
        {
            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";
            // _connectionString = "Server=localhost;Database=db_py_unlz;Uid=root;Pwd=admin;";
        }


        public IEnumerable<Evento> ObtenerEventos()
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Evento> evento = db.Query<Evento>("SELECT * FROM eventos WHERE Borrado = 0").ToList();
                return evento;
            }
        }

        public IEnumerable<EventoViewModel> ObtenerMisEventos(int IdUsuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventoViewModel> evento = db.Query<EventoViewModel>(@"
                            SELECT eventos.*, 
                            EstadosEventos.Descripcion AS EstadoEvento,
                            personas.Nombre AS NombrePersonaAgasajada,
                            tiposeventos.Descripcion AS TipoEvento,
                            Usuarios.NombreCompleto AS NombreUsuario
                            FROM eventos
                            LEFT JOIN EstadosEventos ON EstadosEventos.IdEstadoEvento = eventos.IdEstadoEvento
                            LEFT JOIN personas ON personas.IdPersona = eventos.IdPersonaAgasajada
                            LEFT JOIN tiposeventos ON tiposeventos.IdTipoEvento = eventos.IdTipoEvento
                            LEFT JOIN Usuarios ON Usuarios.IdUsuario = eventos.IdUsuario
                            WHERE eventos.IdUsuario = @IdUsuario",
                            new { IdUsuario = IdUsuario })
                            .ToList();

                //List<EventoViewModel> eventos = db.Query<EventoViewModel>("SELECT Eventos.*, EstadoEventos.Descripcion EstadoEvento FROM Eventos LEFT JOIN EstadoEventos ON EstadoEventos.IdEstadoEvento = Eventos.idEstadoEvento WHERE Eventos.IdUsuario =" + idUsuario.ToString()).ToList();
                return evento;
            }

        }

        public IEnumerable<EventoViewModel> ObtenerEventosViewModel()
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventoViewModel> evento = db.Query<EventoViewModel>("SELECT eventos.*," +
                        "EstadosEventos.Descripcion AS EstadoEvento," +
                        "personas.Nombre AS NombrePersonaAgasajada," +
                        "tiposeventos.Descripcion AS TipoEvento," +
                        "Usuarios.NombreCompleto AS NombreUsuario " +
                        "FROM eventos " +
                        "LEFT JOIN EstadosEventos ON EstadosEventos.IdEstadoEvento = eventos.IdEstadoEvento " +
                        "LEFT JOIN personas ON personas.IdPersona = eventos.IdPersonaAgasajada " +
                        "LEFT JOIN tiposeventos ON tiposeventos.IdTipoEvento = eventos.IdTipoEvento " +
                        "LEFT JOIN Usuarios ON Usuarios.IdUsuario = eventos.IdUsuario").ToList();

                //List<EventoViewModel> evento = db.Query<EventoViewModel>("select eventos. *, EstadosEventos.Descripcion EstadoEvento from eventos left join EstadosEventos on EstadosEventos.IdEstadoEvento = eventos.IdEstadoEvento").ToList();
                return evento;
            }
        }

        public Evento ObtenerEventosId(int IdEvento)
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Evento evento = db.Query<Evento>("SELECT * FROM personas WHERE IdEvento = " + IdEvento.ToString()).First();
                return evento;
            }
        }

        public int AgregarEvento(Evento evento)
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO eventos (NombreEvento,FechaEvento,CantidadPersonas,IdPersonaAgasajada,IdUsuario,IdTipoEvento,IdEstadoEvento) VALUES (@NombreEvento,@FechaEvento,@CantidadPersonas,@IdPersonaAgasajada,@IdUsuario,@IdTipoEvento,@IdEstadoEvento)" + "select  CAST(SCOPE_IDENTITY() AS INT) ";
                evento.IdEvento = db.QuerySingle<int>(query, evento);

                return evento.IdEvento;
            }
        }

        public bool ModificarEvento(int IdEvento, Evento evento)
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE eventos SET NombreEvento= @NombreEvento,FechaEvento=@FechaEvento,CantPersonas=@CantPersonas,IdUsuario=@IdUsuario,IdPersonaContacto=@IdPersonaContacto,IdTipoDespedida=@IdTipoDespedida,IdEstadoEvento=@IdEstadoEvento WHERE IdEvento = " + IdEvento.ToString();
                db.Execute(query, evento);
                return true;
            }
        }

        public bool BorradoLogicoEvento(int IdEvento)
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE eventos SET borrado = 1 WHERE IdEvento = " + IdEvento.ToString();
                db.Execute(query);
                return true;
            }
        }

        public bool CambiarEstadoEvento(int IdEvento, int IdEstadoEvento)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Eventos SET IdEstadoEvento = " + IdEstadoEvento.ToString() + " WHERE IdEvento = " + IdEvento.ToString();


                    db.Execute(query);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                // Console.WriteLine(ex.Message);

                return false;
            }

            /*
            public bool BorradoFisicoEvento(int IdEvento)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "" + IdEvento.ToString();
                    db.Execute(query);
                    return true;
                }
            }*/

        }
    } 
}

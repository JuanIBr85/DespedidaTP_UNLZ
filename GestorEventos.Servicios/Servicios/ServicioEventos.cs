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
        Evento ObtenerEventosId(int IdEvento);
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
                string query = "INSERT INTO eventos (NombreEvento,FechaEvento,CantidadPersonas,IdPersonaAgasajada,IdUsuario,IdTipoEvento,IdEstadoEvento) VALUES (@NombreEvento,@FechaEvento,@CantidadPersonas,@IdPersonaAgasajada,@IdUsuario,@IdTipoEvento,@IdEstadoEvento)";
                db.Execute(query, evento);
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

using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventosServiciosService
    {
        IEnumerable<EventosServicios> GetServiciosPorEvento(int IdEvento);
        int PostNuevoEventoServicio(EventosServicios relacionEventoServicio);
    }
    public class EventosServiciosService : IEventosServiciosService
    {
        private string _connectionString;



        public EventosServiciosService()
        {

            //Connection string 
            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";

        }
        public int PostNuevoEventoServicio(EventosServicios relacionEventoServicio)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO eventosservicios(IdEvento, IdServicio) OUTPUT INSERTED.IdEventoServicio VALUES(@IdEvento, @IdServicio)";
                    int idEventoServicio = db.QuerySingle<int>(query, relacionEventoServicio);

                    return idEventoServicio;
                }
            }
            catch (Exception ex)
            {
                // Registrar el error (puedes usar alguna librería de logging, por ejemplo, log4net, NLog, etc.)
                // Log.Error(ex, "Error al insertar en la tabla EventosServicios");
                return 0;
            }
        }


        public IEnumerable<EventosServicios> GetServiciosPorEvento(int IdEvento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventosServicios> eventos = db.Query<EventosServicios>("SELECT * FROM eventosservicios WHERE IdEvento =" + IdEvento.ToString()).ToList();

                return eventos; 

            }
        }

    }



}

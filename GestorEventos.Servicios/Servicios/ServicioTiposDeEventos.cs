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
    public interface IServicioTiposDeEventos
    {
        IEnumerable<TipoEvento> GetTipoEventos();
        TipoEvento GetTipoEventoId(int IdEvento);

    }
    public class ServicioTiposDeEventos : IServicioTiposDeEventos
    {
        private string _connectionString;

        public ServicioTiposDeEventos()
        {
            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";
            // _connectionString = "Server=localhost;Database=db_py_unlz;Uid=root;Pwd=admin;";
        }


        public IEnumerable<TipoEvento> GetTipoEventos()
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<TipoEvento> evento = db.Query<TipoEvento>("SELECT * FROM TiposEventos WHERE Borrado = 0").ToList();
                return evento;
            }
        }

        public TipoEvento GetTipoEventoId(int IdEvento)
        {
            // using (MySqlConnection db = new MySqlConnection(_connectionString))
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                TipoEvento evento = db.Query<TipoEvento>("SELECT * FROM TiposEventos WHERE IdTipoEvento = " + IdEvento.ToString()).First();
                return evento;
            }
        }
    }
}

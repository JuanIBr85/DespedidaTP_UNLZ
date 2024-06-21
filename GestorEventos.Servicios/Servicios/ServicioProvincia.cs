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
    public interface IServicioProvincia
    {
        /*bool AgregarProvincia(Provincia provincia);
        bool BorradoFisicoProvincia(int IdProvincia);
        bool BorradoLogicoProvincia(int IdProvincia);
        bool ModificarProvincia(int IdProvincia, Provincia provincia);*/
        IEnumerable<Provincia> ObtenerProvincia();
        Provincia ObtenerProvinciaId(int IdProvincia);
    }

    public class ServicioProvincia : IServicioProvincia
    {

        private string _connectionString;

        public ServicioProvincia()
        {
            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";
            // _connectionString = "Server=localhost;Database=db_py_unlz;Uid=root;Pwd=admin;";
        }


        public IEnumerable<Provincia> ObtenerProvincia()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                List<Provincia> provincia = db.Query<Provincia>("SELECT * FROM Provincia").ToList();
                return provincia;
            }
        }

        public Provincia ObtenerProvinciaId(int IdProvincia)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                Provincia provincia = db.Query<Provincia>("SELECT * FROM Provincia WHERE IdProvincia = " + IdProvincia.ToString()).First();
                return provincia;
            }
        }


        /*
        public bool AgregarProvincia(Provincia provincia)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                string query = "";
                db.Execute(query, provincia);
                return true;
            }

        }

        public bool ModificarProvincia(int IdProvincia, Provincia provincia)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                string query = "" + IdProvincia.ToString();
                db.Execute(query, provincia);
                return true;
            }
        }

        public bool BorradoLogicoProvincia(int IdProvincia)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                string query = "" + IdProvincia.ToString();
                db.Execute(query);
                return true;
            }
        }

        public bool BorradoFisicoProvincia(int IdProvincia)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            //using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                string query = "" + IdProvincia.ToString();
                db.Execute(query);
                return true;
            }
        }*/

    }
}

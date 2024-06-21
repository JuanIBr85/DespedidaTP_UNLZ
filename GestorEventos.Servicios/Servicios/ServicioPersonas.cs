    using Dapper;
    using GestorEventos.Servicios.Entidades;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace GestorEventos.Servicios.Servicios
    {
        public interface IServicioPersonas
        {
            int AgregarPersona(Personas personas);
            //bool BorradoFisicoPersona(int IdPersona);
            bool BorradoLogicoPersona(int IdPersona);
            bool DesacerBorradoLogicoPersona(int IdPersona);
            bool ModificarPersona(int IdPersona, Personas personas);
            Personas? ObtenerIdPersona(int IdPersona);
            IEnumerable<Personas> ObtenerPersonas();
        }

        public class ServicioPersonas : IServicioPersonas
        {
            //IEnumerable se utiliza para generar una lista de objetos

            private string _connectionString;

            public ServicioPersonas()
            {

            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";
                                    //"Data Source=Jimi-Floyd\\SQLEXPRESS;Initial Catalog=BDDespedidas;User ID=sa;Password=12345678;Persist Security Info=True";
                                    // _connectionString = "Server=localhost;Database=db_py_unlz;Uid=root;Pwd=admin;";
                                    //"Password=admin;Persist Security Info=True;User ID=root;Initial Catalog=db_py_unlz;Data Source=MYSQL";
            }
            public IEnumerable<Personas> ObtenerPersonas()
            {

                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    List<Personas> personas = db.Query<Personas>("SELECT * FROM personas WHERE Borrado = 0").ToList();
                    return personas;
                }


            }

            public Personas? ObtenerIdPersona(int IdPersona)
            {

                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    Personas personas = db.Query<Personas>("SELECT * FROM personas WHERE IdPersona = " + IdPersona.ToString()).First();
                    return personas;
                }

            }

            public int AgregarPersona(Personas personas)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO personas(Nombre,Apellido,Email,Telefono, DireccionCalle, DireccionNumero, DireccionPiso, DireccionDepartamento, DireccionCodPostal) VALUES(@Nombre,@Apellido,@Email,@Telefono,@DireccionCalle,@DireccionNumero,@DireccionPiso,@DireccionDepartamento, @DireccionCodPostal)" + "select CAST(SCOPE_IDENTITY() AS INT)";
                    personas.IdPersona = db.QuerySingle<int>(query, personas);
                    return personas.IdPersona;
                }
            }

            public bool ModificarPersona(int IdPersona, Personas personas)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE personas SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono, DireccionCalle = @DireccionCalle, DireccionNumero = @DireccionNumero, DireccionPiso = @DireccionPiso, DireccionDepartamento = @DireccionDepartamento WHERE IdPersona = " + IdPersona.ToString();
                    db.Execute(query, personas);
                    return true;
                }
            }

            public bool BorradoLogicoPersona(int IdPersona)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE personas SET Borrado = 1 WHERE IdPersona = " + IdPersona.ToString();
                    db.Execute(query);
                    return true;
                }
            }

            public bool DesacerBorradoLogicoPersona(int IdPersona)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE personas SET Borrado = 0 WHERE IdPersona = " + IdPersona.ToString();
                    db.Execute(query);
                    return true;
                }
            }
            /*       
            public bool BorradoFisicoPersona(int IdPersona)
            {
                // using (MySqlConnection db = new MySqlConnection(_connectionString))
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM personas WHERE IdPersona = " + IdPersona.ToString();
                    db.Execute(query);
                    return true;
                }
            }*/

        }
    }

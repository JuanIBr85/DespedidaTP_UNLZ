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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorEventos.Servicios.Servicios
{
    public interface IServicioUsuarios
    {
        public IEnumerable<Usuario> GetUsuarios();
        public Usuario GetUsuarioPorId(int IdUsuario);
        public Usuario GetUsuarioPorGoogleSubject(string googleSubject);
        public int AgregarNuevoUsuario(Usuario usuario);

    }

    public class ServicioUsuarios : IServicioUsuarios
    {

        private string _connectionString;

        public ServicioUsuarios()
        {
            _connectionString = "Password=Jimifloyd_22;Persist Security Info=True;User ID=Administrrador;Initial Catalog=DespedidaDeSolteros-DB;Data Source=despedidadesolteros-server.database.windows.net";
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Usuario> usuarios = db.Query<Usuario>("SELECT * FROM Usuarios").ToList();

                return usuarios;

            }

            //			return PersonasDePrueba;
        }

        public Usuario GetUsuarioPorId(int IdUsuario)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuarios WHERE IdUsuario = " + IdUsuario.ToString()).FirstOrDefault();

                return usuarios;
            }


        }

        public Usuario GetUsuarioPorGoogleSubject(string googleSubject)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuarios WHERE GoogleIdentificador = '" + googleSubject.ToString() + "'").FirstOrDefault();

                return usuarios;
            }
        }
        public int AgregarNuevoUsuario(Usuario usuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (GoogleIdentificador, Nombre, Apellido, NombreCompleto,  Email) VALUES ( @GoogleIdentificador, @Nombre, @Apellido, @NombreCompleto, @Email); SELECT CAST(SCOPE_IDENTITY() AS int)";
                int idUsuario = (int)db.ExecuteScalar(query, usuario);


                return idUsuario;
            }
        }

    }
}

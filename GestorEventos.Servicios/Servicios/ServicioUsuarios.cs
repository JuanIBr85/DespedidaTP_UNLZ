using Dapper;
using GestorEventos.Servicios.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public interface IServicioUsuarios
    {
        bool RegistrarUsuario(Usuario usuario);
        string ValidarUsuario(string nombre_usuario, string clave_usuario);
    }

    /*public class ServicioUsuarios : IServicioUsuarios
    {

        private string _connectionString;

        public ServicioUsuarios()
        {
            _connectionString = "Server=localhost;Database=db_py_unlz;Uid=root;Pwd=admin;";
        }

        public string ValidarUsuario(string nombre_usuario, string clave_usuario)
        {
            using (MySqlConnection db = new MySqlConnection(_connectionString))
            {
                List<Usuario> usuario = db.Query<Usuario>("SELECT * FROM usuarios WHERE Nombre LIKE '" + nombre_usuario + "' AND Clave LIKE '" + clave_usuario + "'").ToList();
                
                if (usuario.Count == 0)
                {
                    return "Usuario Inexistente";
                }
                else
                {
                    if (usuario[0].tipo_usuario)
                    {
                        return "Administrador";
                    }
                    else
                    {
                        return "Usuario";
                    }
                   
                }
            }
        }

        public bool RegistrarUsuario(Usuario usuario)
        {

            string resultado = ValidarUsuario(usuario.nombre_usuario, usuario.clave_usuario);

            if (resultado == "Usuario Inexistente")
            {
                return false;
            }

            else
            {
                using (MySqlConnection db = new MySqlConnection(_connectionString))
                {
                    string query = "INSERT INTO usuarios(Nombre, Clave, Tipo_Usuario) VALUES(@nombre_usuario, @clave_usuario, @tipo_usuario)";
                    db.Execute(query, usuario);
                    return true;
                }
            }

        }

    }*/
}

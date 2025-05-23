using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Bussiness
{
    public class LoginBussiness
    {
        LoginDAO dao = new LoginDAO();
        public bool ValidarUsuario(string email, string password)
        {

            return dao.ValidarUsuario(email, password);
        }

        public string RegistrarUsuario(Usuario obj)
        {
            try
            {
                return dao.RegistrarUsuario(obj);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public string ActualizarUsuario(Usuario obj)
        {
            try
            {
                dao.ActualizarUsuario(obj);

                return "Usuario actualizado correctamente";

            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public List<Usuario> ListatUsuarios()
        {
            return dao.ListatUsuarios();
        }
    }
}

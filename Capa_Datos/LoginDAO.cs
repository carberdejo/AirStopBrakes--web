using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Capa_Entidad;

namespace Capa_Datos
{
    public class LoginDAO
    {

        public bool ValidarUsuario(string email, string password) {

            DataTable dt = DBHelper.RetornaDataTable("SP_VALIDAR_USUARIO", email, password);
            if(dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                //string rol = dr["cod_rol"].ToString();

                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                //        1, rol, DateTime.Now, DateTime.Now.AddMinutes(30), false, email
                //    );
                //string encriptado = FormsAuthentication.Encrypt(ticket);

                //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encriptado);
                //HttpContext.Current.Response.Cookies.Add(cookie);

                HttpContext.Current.Session["codigo"] = dr["cod_usu"];
                HttpContext.Current.Session["correo"] = dr["cor_usu"];
                HttpContext.Current.Session["nombre"] = dr["nom_usu"];
                HttpContext.Current.Session["rol"] = dr["cod_rol"];
                HttpContext.Current.Session["email"] = dr["cor_usu"];

                return true;
            }
            return false;
        }

        public string RegistrarUsuario(Usuario obj)
        {
            try
            {
                DataTable dt = DBHelper.RetornaDataTable("PA_REGISTRAR_USUARIO", obj.nom_usu, obj.tel_usu, obj.cor_usu,
                                                         obj.pass_usu, obj.fec_nac, obj.cod_dist);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    HttpContext.Current.Session["codigo"] = dr["codigo"];
                    HttpContext.Current.Session["correo"] = obj.cor_usu;
                    HttpContext.Current.Session["nombre"] = obj.nom_usu;
                    HttpContext.Current.Session["rol"] = dr["rol"];


                    return "Usuario registrado correctamente";
                }
                return "No se registro";
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

    }
}

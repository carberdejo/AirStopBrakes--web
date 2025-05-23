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
        string RolUsuario = "R01";
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
                HttpContext.Current.Session["apellido"] = dr["ape_usu"];

                return true;
            }
            return false;
        }

        public string RegistrarUsuario(Usuario obj)
        {
            try
            {
                object codUsuario = DBHelper.EjecutarSP_TRX_Object("PA_REGISTRAR_USUARIO", obj.nom_usu,obj.ape_usu, obj.tel_usu, obj.cor_usu,
                                                         obj.pass_usu, obj.fec_nac, obj.cod_dist);

                    string codigo = codUsuario.ToString();
                HttpContext.Current.Session["codigo"] = codigo;
                    HttpContext.Current.Session["correo"] = obj.cor_usu;
                    HttpContext.Current.Session["nombre"] = obj.nom_usu;
                    HttpContext.Current.Session["apellido"] = obj.ape_usu;
                    HttpContext.Current.Session["rol"] = RolUsuario;
                    HttpContext.Current.Session["email"] = obj.cor_usu;


                    return $"Usuario {codigo} registrado correctamente";
                
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public List<Usuario> ListatUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_USUARIO");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(json);
        
            return lista;
        }

        public string ActualizarUsuario(Usuario obj)
        {
            try
            {
                DBHelper.EjecutarSP("SP_UPDATE_USUARIO", obj.cod_usu, obj.nom_usu,obj.ape_usu ,obj.tel_usu, obj.cor_usu,
                                                         obj.pass_usu, obj.fec_nac, obj.cod_dist);

                HttpContext.Current.Session["correo"] = obj.cor_usu;
                HttpContext.Current.Session["nombre"] = obj.nom_usu;
                HttpContext.Current.Session["apellido"] = obj.ape_usu;

                return "Usuario actualizado correctamente";
                              
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capa_Bussiness;
using Capa_Entidad;

namespace ProyWebRepuestosFrenosDeAire.Controllers
{
    public class LoginController : Controller
    {
        LoginBussiness log = new LoginBussiness();

        // GET: Login
        public ActionResult Login()
        {

            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Login(Usuario obj)
        {
            if (ModelState.IsValid)
            {
                bool registrado = log.ValidarUsuario(obj.cor_usu, obj.pass_usu);
                if (registrado)
                {
                    
                    Console.WriteLine("Email: " + obj.cor_usu);
                    Console.WriteLine("Password: " + obj.pass_usu);
                    return RedirectToAction("IndexProductoCliente", "Producto");
                }
                else
                {
                    ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
                    return View();
                }
            }
            return View(obj);
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Registrar()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Registrar(Usuario obj) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = log.RegistrarUsuario(obj);
                    return RedirectToAction("IndexProductoCliente","Producto");
                }
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            //ViewBag.listProv = new SelectList(prodBussiness.listaProveedor(), "cod_prov", "nom_prov");
            //ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View(obj);
        }
    }
}
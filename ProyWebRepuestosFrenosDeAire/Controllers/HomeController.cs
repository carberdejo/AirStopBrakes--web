using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Bussiness;
using Capa_Entidad;
using Newtonsoft.Json;


namespace ProyWebRepuestosFrenosDeAire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Carrito> listCarrito = new List<Carrito>();
            //Si la variable de session no existe
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = JsonConvert.SerializeObject(listCarrito);
            }
            return View();
        }

        public ActionResult IndexProductoCategoria(string id = "CA001" ,string nombre = "")
        {
            ProductoBussiness prodBussiness = new ProductoBussiness();
            
            var lista = prodBussiness.listaProductoCate(id, nombre);
            return View(lista);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
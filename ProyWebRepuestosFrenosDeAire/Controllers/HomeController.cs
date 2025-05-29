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
        List<Carrito> listCarrito = new List<Carrito>();
        void CargarCarrito()
        {
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = JsonConvert.SerializeObject(listCarrito);
            }
        }
        public ActionResult Index()
        {
            //Si la variable de session no existe
            CargarCarrito();
            return View();
        }

        public ActionResult IndexProductoCliente( string nombre = "")
        {
            CargarCarrito();
            ProductoBussiness prodBussiness = new ProductoBussiness();

            var lista = prodBussiness.listaProducto( nombre);
            return View(lista);
        }

        public ActionResult IndexProductoCategoria(string id_cate = "CA001" ,string nombre = "")
        {
            CargarCarrito();
            ProductoBussiness prodBussiness = new ProductoBussiness();
            
            var lista = prodBussiness.listaProductoCate(id_cate, nombre);
            return View(lista);
        }
        public ActionResult AcercaDe()
        {
            CargarCarrito();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
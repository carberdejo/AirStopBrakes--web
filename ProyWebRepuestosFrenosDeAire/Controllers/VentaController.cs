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
    public class VentaController : Controller
    {
        VentasBussiness vent_buss = new VentasBussiness();
        ProductoBussiness pro = new ProductoBussiness();
        //
        List<Carrito>listCarrito = new List<Carrito>();

        void getCarrito()
        {
            listCarrito = JsonConvert.DeserializeObject<List<Carrito>>(Session["Carrito"].ToString());
        }
        void setCarrito()
        {
            Session["Carrito"]= JsonConvert.SerializeObject(listCarrito);
        }

        // GET: List Carrito
        public ActionResult IndexCarrito()
        {
            getCarrito();
            if(listCarrito.Count == 0)
            {
                return RedirectToAction("IndexProductoCliente","Producto");
            }
            ViewBag.total = listCarrito.Sum(p => p.importe);
            return View(listCarrito);
        }

        // GET: Venta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venta/AgregarProducto
        public ActionResult AgregarProducto(string id = "")
        {
            
            var buscado = pro.listaProducto("").Find(x => x.cod_produc.Equals(id));
            return View(buscado);
        }

        // POST: Venta/AgregarProducto
        [HttpPost]
        public ActionResult AgregarProducto(string id = "", int cantidad = 1)
        {
            if (Session["codigo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var buscado = pro.listaProducto("").Find(x => x.cod_produc.Equals(id));
            getCarrito();
            
            var encontrado = listCarrito.Find(c=>c.cod_produc.Equals(id));
            if(encontrado == null)
            {
                Carrito car = new Carrito()
                {
                    cod_produc = id,
                    nom_pro = buscado.nom_pro,
                    precio = buscado.pre_pro,
                    cantidad = cantidad
                };
                listCarrito.Add(car);
                ViewBag.mensaje = $"Se agrego el producto : {buscado.nom_pro}" + $" con la cantidad {cantidad}";
            }
            else
            {
                int ncant = encontrado.cantidad;
                encontrado.cantidad += cantidad;
                ViewBag.mensaje = $"Se aumento el producto : {buscado.nom_pro}" + $" con la cantidad {ncant} a {encontrado.cantidad}";
            }

            setCarrito();
            return View(buscado);
        }

        // GET: Venta/Edit/5
        public ActionResult EliminarItemCarrito(string id)
        {
            getCarrito() ;
            Carrito eliminar = listCarrito.Find(c => c.cod_produc.Equals(id));
            listCarrito.Remove(eliminar);

            TempData["Mensaje"] = $"Se eliminó al Articulo {eliminar.nom_pro}";

            if (eliminar.cantidad > 0)
            {
                eliminar.cantidad--;
                //
                TempData["Mensaje"] =
                    $"Se actualizó la cantidad del Articulo: {eliminar.nom_pro}";
            }
            setCarrito() ;
            return RedirectToAction("IndexCarrito"); ;
        }

        // GET: Venta/PagarCarrito
        public ActionResult PagarCarrito()
        {
            getCarrito();
            ViewBag.contador = listCarrito.Count;
            ViewBag.suma_importe = listCarrito.Sum(c => c.importe);
            //
            return View(listCarrito);
        }

        // POST: Venta/PagarCarrito
        [HttpPost]
        public ActionResult PagarCarrito(string id, FormCollection collection)
        {
            try
            {
                getCarrito();
                decimal ven_total = listCarrito.Sum(c => c.importe);
                //
                string numVenta= vent_buss.GrabarVenta(id, ven_total);
                //
                foreach (var item in listCarrito)
                {
                    vent_buss.GrabarDatalleVenta(numVenta, item);
                }
                //
                Session.Remove("Carrito");
                //
                ViewBag.Mensaje = $"El Nro. de Venta: {numVenta} " +
                                  "fué realizada con éxito";

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            ViewBag.contador = listCarrito.Count;
            ViewBag.suma_importe = listCarrito.Sum(c => c.importe);
            return View(listCarrito);
        }

       
    }
}

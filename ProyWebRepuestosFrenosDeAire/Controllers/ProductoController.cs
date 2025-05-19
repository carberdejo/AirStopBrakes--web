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
    //[Authorize(Roles = "R02")]
    public class ProductoController : Controller
    {

        ProductoBussiness prodBussiness = new ProductoBussiness();
        List<Carrito> listCarrito = new List<Carrito>();
        Producto buscarProduc(string id)
        {
            var buscar = prodBussiness.listaProducto("").Find( p => p.cod_produc.Equals(id));
            return buscar;
        }



        // GET: Producto
        public ActionResult IndexProducto(string nombre="")
        {
            return View(prodBussiness.listaProducto(nombre));
        }

        
        public ActionResult IndexProductoCliente(string nombre = "")
        {
            //Si la variable de session no existe
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = JsonConvert.SerializeObject(listCarrito);
            }
            return View(prodBussiness.listaProducto(nombre));
        }

        // GET: Producto/Details/5
        public ActionResult Details(string id)
        {
            return View(buscarProduc(id));
        }

        // GET: Producto/Create
        public ActionResult CreateProducto()
        {
            ViewBag.listProv = new SelectList(prodBussiness.listaProveedor(), "cod_prov", "nom_prov");
            ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View(new Producto());
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult CreateProducto(Producto obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TempData["mensaje"]= prodBussiness.GrabarProducto(obj);
                    return RedirectToAction("IndexProducto");
                }
            }
            catch (Exception e) 
            {
                ViewBag.mensaje = e.Message;
            }
            ViewBag.listProv = new SelectList(prodBussiness.listaProveedor(), "cod_prov", "nom_prov");
            ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View(obj);
        }

        // GET: Producto/Edit/5
        public ActionResult EditProducto(string id)
        {
            ViewBag.listProv = new SelectList(prodBussiness.listaProveedor(), "cod_prov", "nom_prov");
            ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View( buscarProduc(id) );
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult EditProducto(string id, Producto obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TempData["mensaje"] = prodBussiness.UpdateProducto(obj);
                    return RedirectToAction("IndexProducto");
                }
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            ViewBag.listProv = new SelectList(prodBussiness.listaProveedor(), "cod_prov", "nom_prov");
            ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View(obj);
        }

        // GET: Producto/Delete/5
        public ActionResult DeleteProducto(string id)
        {
            return View(buscarProduc(id));
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult DeleteProducto(string id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = prodBussiness.DeleteProducto(id);
                    return RedirectToAction("IndexProducto");
                }
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            return View(buscarProduc(id));
        }
    }
}

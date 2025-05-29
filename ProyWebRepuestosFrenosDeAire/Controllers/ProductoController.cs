using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Bussiness;
using Capa_Entidad;
using Newtonsoft.Json;
using ProyWebRepuestosFrenosDeAire.Permisos;

namespace ProyWebRepuestosFrenosDeAire.Controllers
{
    [ValidarSession]
    public class ProductoController : Controller
    {

        ProductoBussiness prodBussiness = new ProductoBussiness();
        ProveedorBussiness provB = new ProveedorBussiness();

        List<Carrito> listCarrito = new List<Carrito>();
        Producto buscarProduc(string id)
        {
            var buscar = prodBussiness.listaProducto("").Find(p => p.cod_produc.Equals(id));
            return buscar;
        }



        // GET: Producto
        public ActionResult IndexProducto( int nro_pag=0,string nombre = "" )
        {
            var lista = prodBussiness.listaProducto(nombre);
            int num = lista.Count;
            //
            ViewBag.contador = num;
            ViewBag.nombre = nombre;
            int cant_filas = 8;
            //
            int cant_pag= (num % cant_filas == 0) ? (num / cant_filas) : (num / cant_filas + 1);
            //
            ViewBag.CANT_PAGINAS = cant_pag;

            return View(lista.Skip(nro_pag*cant_filas).Take(cant_filas));
        }


        // GET: Producto/Details/5
        public ActionResult DetailsStock(string id)
        {
            return View(buscarProduc(id));
        }
        // POST: Producto/Details/5
        [HttpPost]
        public ActionResult DetailsStock(string id, int newStock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = prodBussiness.UpdateStockProducto(id, newStock);
                    return RedirectToAction("IndexProducto");
                }
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            return View();
        }

        // GET: Producto/Create
        public ActionResult CreateProducto()
        {
            ViewBag.listProv = new SelectList(provB.listaProveedor(""), "cod_prov", "nom_prov");
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
            ViewBag.listProv = new SelectList(provB.listaProveedor(""), "cod_prov", "nom_prov");
            ViewBag.listCate = new SelectList(prodBussiness.listaCategoria(), "cod_cate", "nom_cate");
            return View(obj);
        }

        // GET: Producto/Edit/5
        public ActionResult EditProducto(string id)
        {
            ViewBag.listProv = new SelectList(provB.listaProveedor(""), "cod_prov", "nom_prov");
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
            ViewBag.listProv = new SelectList(provB.listaProveedor(""), "cod_prov", "nom_prov");
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

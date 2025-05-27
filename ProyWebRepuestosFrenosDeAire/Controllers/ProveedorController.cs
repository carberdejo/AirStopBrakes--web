using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Bussiness;
using Capa_Entidad;
using ProyWebRepuestosFrenosDeAire.Permisos;

namespace ProyWebRepuestosFrenosDeAire.Controllers
{
    [ValidarSession]
    public class ProveedorController : Controller
    {
        ProveedorBussiness prov = new ProveedorBussiness();

        Proveedor buscarProv(string id)
        {
            var buscar = prov.listaProveedor("").Find(p => p.cod_prov.Equals(id));
            return buscar;
        }

        // GET: Proveedor
        public ActionResult IndexProveedor(string nombre="")
        {
            return View(prov.listaProveedor(nombre));
        }

        // GET: Proveedor/DetailsProveedor/5
        public ActionResult DetailsProveedor(string id)
        {
            return View(buscarProv(id));
        }

        // GET: Proveedor/Create
        public ActionResult CreateProveedor()
        {
            return View(new Proveedor());
        }

        // POST: Proveedor/Create
        [HttpPost]
        public ActionResult CreateProveedor(Proveedor obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = prov.GrabarProveedor(obj);

                    return RedirectToAction("IndexProveedor");
                }
            }
            catch(Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            
            return View(obj);
        }

        // GET: Proveedor/Edit/5
        public ActionResult EditProveedor(string id)
        {
            return View(buscarProv(id));
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult EditProveedor(string id, Proveedor obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TempData["mensaje"] = prov.UpdateProveedor(obj);
                    return RedirectToAction("IndexProveedor");
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.mensaje = e.Message;
            }

            return View(obj);
        }

        // GET: Proveedor/Delete/5
        public ActionResult DeleteProveedor(string id)
        {
            return View(buscarProv(id));
        }

        // POST: Proveedor/Delete/5
        [HttpPost]
        public ActionResult DeleteProveedor(string id, FormCollection collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TempData["mensaje"] = prov.DeleteProveedor(id);
                    return RedirectToAction("IndexProveedor");
                }
            }
            catch(Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            return View(buscarProv(id));
        }
    }
}

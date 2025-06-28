using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Bussiness;

namespace ProyWebRepuestosFrenosDeAire.Controllers
{
    public class ReporteController : Controller
    {
        ReporteBusiness bus = new ReporteBusiness();

        // GET: Reporte
        public ActionResult UsuarioVentas(int cant=0)
        {
            return View(bus.ListUsuVent(cant));
        }

        // GET: Reporte/Details/5
        public ActionResult ProductoImporteVentas(decimal imp = 0)
        {
            return View(bus.ListProVent(imp));
        }

        // GET: Reporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporte/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    
    }
}

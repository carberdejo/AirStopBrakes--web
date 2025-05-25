using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyWebRepuestosFrenosDeAire.Permisos
{
    public class ValidarSessionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string rol = HttpContext.Current.Session["rol"] as string;
            if (rol == null || rol == "R02")
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
    
    
}
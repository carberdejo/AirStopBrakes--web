using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyWebRepuestosFrenosDeAire.Permisos
{
    public class ValidarSession: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["rol"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
    
    
}
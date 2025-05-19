using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ProyWebRepuestosFrenosDeAire
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    HttpCookie authcookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        //    if (authcookie != null)
        //    {
        //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authcookie.Value);

        //        if (ticket != null && !ticket.Expired)
        //        {
        //            //string[] datos = ticket.UserData.Split('|');

        //            FormsIdentity identity = new FormsIdentity(ticket);
        //            string[] roles = new string[] { ticket.UserData };
        //            GenericPrincipal principal = new GenericPrincipal(identity,roles);

        //            HttpContext.Current.User = principal;
        //        }
        //    }
        //}


    }
}

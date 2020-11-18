using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Filters
{
    public class LoginRequiredFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // on récupère l'utillisateur de la session
            
            UserDTO ap = (UserDTO)filterContext.HttpContext.Session["userConnected"];

            // si l'apprenant n'est pas connecté
            if (ap == null)
            {
                //filterContext.HttpContext.Session["LastUrl"] = filterContext.HttpContext.Request.RawUrl;
                // On redirive vers /Authentification/Login
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        {"controller","Authentification"},
                        {"action","Login"},
                        {"redirectTo", filterContext.HttpContext.Request.RawUrl}
                    }
                );
            }
        }
    }
}
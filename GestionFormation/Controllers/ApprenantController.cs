using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Filters;
using Newtonsoft.Json;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class ApprenantController : Controller
    {
        // GET: Apprenant
        public static UserDTO userconnected;
        [LoginRequiredFilter]
        
        public ActionResult Index()
        {
            // je dois afficher la page web get byID de web service
            UserDTO ap = new UserDTO();


            ap = (UserDTO)Session["userConnected"];
            userconnected = ap; 
        //(((GestionFormation.DTO.UserDTO)Session["userConnected"]).Prenom)


            // (((GestionFormation.DTO.UserDTO)Session["userConnected"]).Email)


            return View();
        }

        public ActionResult feuillePresence()
        {
            //int id = ((GestionFormation.DTO.UserDTO)Session["userConnected"]).Id;
            //UserDTO ap = new UserDTO();
           
                //ap.Nom = ((Apprenant)Session["userConnected"]).Nom;
                //ap.Prenom = ((Apprenant)Session["userConnected"]).Prenom;
                //ap.Prenom = ((Apprenant)Session["userConnected"]).Email;
                //ap = (Apprenant)Session["userConnected"];
                //ap = (UserDTO)(System.Web.HttpContext.Current.Session["userConnected"]);
            
            return View(userconnected);
        }

       public ActionResult PrintFPresence()
        {
            var q = new ActionAsPdf("feuillePresence");
            return q; 
        }


    }
}

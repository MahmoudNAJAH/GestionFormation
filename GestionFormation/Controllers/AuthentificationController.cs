using GestionFormation.DAO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class AuthentificationController : Controller
    {
        // GET: Authentification : donc l'authentfication controller est le controlleur qui me permet 
        //de faire la page de connexion 


        public static List<Apprenant> LesApprenants = ApprenantDAO.FindAll();
        //1-Donc Maintenant j'ai la liste des Apprenantet surtout leur Login( EMAIL et MOTDEPASSE)


        public ActionResult Login(string redirectTo = null)
        {
            ViewBag.Referer = redirectTo;
            return View(new Apprenant());
        }


        [HttpPost]

        public ActionResult Login(Apprenant p, string referer)
        {
            Apprenant ap = ApprenantDAO.FindByLgMD(p.Email);
            // ici j'ai recupéréer l'apprenant qui corresspond au mem mail et password entré dans la 
            //fonction Login
           
            if (ap != null && ap.MotDePasse==p.MotDePasse)
            {
                //session =  propriété du controleur
                // elle spécifique à un utilisateur qur un navigateur (stocké coté serveur)
                //Donc Pas partagé entre les utilisateurs 
                Session.Add("userConnected", ap);

                if (!string.IsNullOrEmpty(referer)) return Redirect(referer);
                else RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Erreuur de connexion ";
            }
            ViewBag.Referer = referer;
            return View();
        }


        public ActionResult Logout()
        {
            //UtilisateursConnectes.Remove((User)Session["userConnected"]);

            //// 1ere option, on passe à null toutes les variable de session
            ////Session[userConnected] = null;

            //// 2è option (préférable) on supprime toutes les données de session (pour cet utilisateur)
            //Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}
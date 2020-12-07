using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
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
            return View(new LoginDTO());
        }


        [HttpPost]
        public ActionResult Login(LoginDTO p , string referer)
        {
            UserDTO user = new UserDTO { Role = UserRole.ATTENDANT };

            user.GetUserFromEmail(p.Email);
                        
            // ici j'ai recupéréer l'apprenant qui corresspond au mem mail et password entré dans la 
            //fonction Login

            //CryptageMotDePasse hash2 = new CryptageMotDePasse(p.MotDePasse);
            //byte[] hashBytes2 = hash2.ToArray();

            //Apprenant apprenant = new Apprenant();
            //apprenant.MotDePasse = hashBytes2;
            //ApprenantDAO.Create(apprenant);
            if (user.MotDePasse != null )
            {
                //session =  propriété du controleur
                // elle spécifique à un utilisateur qur un navigateur (stocké coté serveur)
                //Donc Pas partagé entre les utilisateurs 


                byte[] hashBytes = user.MotDePasse;//read from store.
                CryptageMotDePasse hash = new CryptageMotDePasse(hashBytes);

                //if (!hash.Verify(p.MotDePasse))
                //    throw new System.UnauthorizedAccessException();


                Session.Add("userConnected", user);

                if (!string.IsNullOrEmpty(referer)) return Redirect(referer);
                else RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Erreur de connexion ";
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
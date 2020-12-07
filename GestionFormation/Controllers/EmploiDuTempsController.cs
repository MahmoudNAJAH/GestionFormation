using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Filters;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    [LoginRequiredFilter]
    public class EmploiDuTempsController : Controller
    {
        //L'emploi du temps de mon utilisateur
        private EmploiDuTempsDTO EDT { get; set; }

        public EmploiDuTempsController()
        {
            //On charge toutes les dates + formations + formateur dans mon controller
            if(!(System.Web.HttpContext.Current.Session["userConnected"] == null || ((UserDTO)System.Web.HttpContext.Current.Session["userConnected"])?.Id == null))
                EDT = new EmploiDuTempsDTO((UserDTO)System.Web.HttpContext.Current.Session["userConnected"]);  
        }

        // GET: EmploiDuTemps
        public ActionResult Index()
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek( EDT.ListDates, DateTime.Now);

            ViewBag.User = (UserDTO)Session["userConnected"]; 

            //Pour affichage et changement de semaine
            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(DateTime.Now);

            //On veut dissocier l'affichage du mois avec la semaine dans le cas ou on change de mois dans le calendrier
            TempData["dateReference"] = DateTime.Now;
            //Pour set le mois de référence, utilisée dans la partie "mois" de l'EDT
            TempData["moisReference"] = DateTime.Now;

            return View(WeeklyEDT);
        }
        
        public ActionResult NextWeek(DateTime dateReference, DateTime? moisReference = null)
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek(EDT.ListDates, dateReference);

            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(dateReference);

            TempData["dateReference"] = dateReference;

            //Si null, on set à la date de référence, ie la date utilisé pour affichage de la semaine
            if (moisReference == null)
                TempData["moisReference"] = dateReference;
            else
                TempData["moisReference"] = moisReference;

            return View("Index", WeeklyEDT);
        }
    }
}
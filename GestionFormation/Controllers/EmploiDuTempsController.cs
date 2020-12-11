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
        public EmploiDuTempsController()
        {
            UserDTO user = (UserDTO)System.Web.HttpContext.Current.Session["userConnected"];

            //On charge toutes l'EDT pour la Session si besoin
            if (System.Web.HttpContext.Current.Session["EDT"] == null)
                System.Web.HttpContext.Current.Session.Add("EDT", new EmploiDuTempsDTO(user));
        }

        // GET: EmploiDuTemps
        public ActionResult Index()
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek( ((EmploiDuTempsDTO)System.Web.HttpContext.Current.Session["EDT"]).ListDates, DateTime.Now);

            ViewBag.User = (UserDTO)Session["userConnected"]; 

            //Pour affichage et changement de semaine
            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(DateTime.Now);

            //Pour set le date reference pour le calendrier
            TempData["dateReference"] = DateTime.Now;
            TempData.Keep("dateReference");

            //Pour set le mois de référence, utilisée dans la partie "mois" de l'EDT
            TempData["moisReference"] = DateTime.Now;
            //On utilise TempData.Keep(), sinon TempData devient null quand on appel Ajax
            TempData.Keep("moisReference");

            return View(WeeklyEDT);
        }
        
        public ActionResult NextWeek(DateTime dateReference)
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek(((EmploiDuTempsDTO)System.Web.HttpContext.Current.Session["EDT"]).ListDates, dateReference);

            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(dateReference);

            TempData["dateReference"] = dateReference;
            TempData.Keep("dateReference");

            TempData["moisReference"] = dateReference;
            TempData.Keep("moisReference");

            return View("Index", WeeklyEDT);
        }

        public ActionResult NextMonth(int id)
        {
            TempData["moisReference"] = ((DateTime)TempData["moisReference"]).AddMonths(id);
            TempData.Keep("moisReference");
            TempData.Keep("dateReference");

            if (TempData["moisReference"] == null) return new EmptyResult();
            else return PartialView("_EdtMonth", TempData["moisReference"]);
        }
    }
}
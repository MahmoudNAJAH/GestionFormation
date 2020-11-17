using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class EmploiDuTempsController : Controller
    {
        //L'emploi du temps de mon utilisateur
        private EmploiDuTempsDTO EDT { get; set; }


        public EmploiDuTempsController()
        {
            //On charge toutes les dates + formations + formateur dans mon controller

            //Pour le moment, on charge l'emploi du temps de Hermione
            
            //EDT = new EmploiDuTempsDTO((UserDTO)Session["userConnected"]);
            EDT = new EmploiDuTempsDTO(new UserDTO { Id = 1, Role = UserRole.ATTENDANT});
        }

        // GET: EmploiDuTemps
        public ActionResult Index()
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek( EDT.ListDates, DateTime.Now);

            //Pour affichage et changement de semaine
            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(DateTime.Now);

            return View(WeeklyEDT);
        }
        
        public ActionResult NextWeek(DateTime dateReference)
        {
            List<JourneeDTO> WeeklyEDT = EmploiDuTempsService.GetWeek(EDT.ListDates, dateReference);

            ViewBag.DateLundi = EmploiDuTempsService.DatePreviousMonday(dateReference);

            return View("Index", WeeklyEDT);
        }
    }
}
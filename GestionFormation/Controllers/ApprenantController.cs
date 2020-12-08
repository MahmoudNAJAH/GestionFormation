using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Filters;
using GestionFormation.Services;
using Newtonsoft.Json;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace GestionFormation.Controllers
{
    public class ApprenantController : Controller
    {
        // GET: Apprenant
        public static UserDTO userconnected;
        //public static  List<FicheEval> FeuilleEvaluation = new List<FicheEval>();
        static Calendar cal = new GregorianCalendar();

        private FicheEval fiche;
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

            string NomFormateur;
            string PrenomFormateur;
            string NomFormation;
            string NomCursus;
            DateTime DateTraitement = new DateTime();
            DateTime FirstDayOfWeek = new DateTime();
            //DateTime dt = new DateTime();
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            FirstDayOfWeek = DateTime.Now.AddDays(-diff).Date;
            FirstDayOfWeek.ToString("dd/mm/yyyy");
            ViewData["date1"] = FirstDayOfWeek.ToString("dd/MM/yyyy");

            DateTime LastDayOfWeek = FirstDayOfWeek.AddDays(6);
            ViewData["date2"] = LastDayOfWeek.ToString("dd/MM/yyyy");


            List<SessionDeFormation> SessionDeFormations = userconnected.GetSessionDeFormations();
            List<SessionDeCursus> sessionDeCursus = userconnected.GetSessionDeCursus();
            foreach (SessionDeFormation SessionForm in SessionDeFormations)
            {
                DateTraitement = SessionForm.DateDebut;
                if (DateTraitement == FirstDayOfWeek || DateTraitement == FirstDayOfWeek.AddDays(1))
                {
                    NomFormateur = SessionForm.Formateur.Nom;
                    ViewBag.Message = NomFormateur;
                    PrenomFormateur = SessionForm.Formateur.Prenom;
                    ViewBag.Message1 = PrenomFormateur;
                    NomFormation = SessionForm.Formation.Nom;
                    ViewBag.Message2 = NomFormation;
                    foreach (SessionDeCursus cursus in sessionDeCursus)
                    {
                        NomCursus = cursus.Cursus.Nom;
                        ViewBag.Message3 = NomCursus;
                    }
                }
            }
            //récupérer le n° de la semaine 
            CultureInfo myCI = new CultureInfo("en-US");

            System.Globalization.Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;

            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            int week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            //Response.Write("Current Week is " + week.ToString());
            ViewData["date"] = week;



            return View(userconnected);
        }


        public ActionResult PrintFPresence()
        {
            var q = new ActionAsPdf("feuillePresence");
            return q;
        }


        public ActionResult feuilleEvaluation()
        {

            return View(new FicheEval());

        }
        //[HttpPost]
        //public ActionResult feuilleEvaluation(FicheEval f)
        //{

        //    return new ActionAsPdf("feuilleEvaluation");

        //}
        [HttpPost]

        public ActionResult PrintEval(FicheEval f)
        {
            //ici je dois renvoyer la lfeuille d'evaluation rempli
            return new ViewAsPdf("feuilleEvaluation", f);
        }



    }
}

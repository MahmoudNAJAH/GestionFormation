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
    [LoginRequiredFilter]
    public class ApprenantController : Controller
    {
        // GET: Apprenant
        public static UserDTO userconnected;
        //public static  List<FicheEval> FeuilleEvaluation = new List<FicheEval>();
        static Calendar cal = new GregorianCalendar();
        public static string NomFormation;

        private FicheEval fiche;
        

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
           
            string NomCursus;
            DateTime DateTraitement = new DateTime();
            DateTime FirstDayOfWeek = new DateTime();
            //DateTime dt = new DateTime();
            //Je récupére la semaine de formation 
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

            //Mainteant je voulais récupérer le nom , le cursus , le nom de formtion , lenom Formateur
            //1)Récupération de la session de formation correspondante au user connected 
            List<SessionDeFormation> SessionDeFormations = userconnected.GetSessionDeFormations();
            //2)récupération de cursus de userconnected 
            List<SessionDeCursus> sessionDeCursus = userconnected.GetSessionDeCursus();
            EmploiDuTempsDTO emploi = new EmploiDuTempsDTO(userconnected);
            List<JourneeDTO> Mesformations = emploi.ListDates;
            //if (DateTraitement == FirstDayOfWeek || DateTraitement == FirstDayOfWeek.AddDays(1))
            DateTime aujourdhui = DateTime.Now;
            foreach(JourneeDTO j in Mesformations)
            {
                //if (j.Date.ToString("dd/mm/yyyy")==DateTime.Now.ToString("dd/mm/yyyy"))

                //DateTime no = new DateTime(1998, 04, 30);
                //if (j.Date.DayOfYear == no.DayOfYear && j.Date.Year == no.Year)
                if (j.Date.DayOfYear == DateTime.Now.DayOfYear && j.Date.Year == DateTime.Now.Year)
                {
                    foreach (SessionDeFormation SessionForm in SessionDeFormations)
                    {

                        DateTraitement = SessionForm.DateDebut;
                        //si la date de début de formation est lundi ou Mardi de la semaine courante je peux afficher 
                        //tout les coordonné concernant l'utilisateur connecté 


                        //if (aujourdhui >= SessionForm.DateDebut && aujourdhui <= (SessionForm.DateDebut).AddDays(SessionForm.Formation.Dure))
                        //{
                        //je vais parcourir la liste des dates 
                        if (j.Formation.FormationId == SessionForm.Formation.FormationId && j.Formateur.FormateurId==SessionForm.Formateur.FormateurId)
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
                }
             }
           
        
            //récupérer le n° de la semaine 
            CultureInfo myCI = new CultureInfo("en-Fr");

            System.Globalization.Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;

            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            int week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            //Response.Write("Current Week is " + week.ToString());
            ViewData["date"] = week;
  
            return View(userconnected);
        }

        [HttpPost]
        public ActionResult PrintFPresence(UserDTO f)
        {
            f = userconnected; 
           string NomFormateur;
            string PrenomFormateur;
           
            string NomCursus;
            DateTime DateTraitement = new DateTime();
            DateTime FirstDayOfWeek = new DateTime();
            //DateTime dt = new DateTime();
            //Je récupére la semaine de formation 
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

            //Mainteant je voulais récupérer le nom , le cursus , le nom de formtion , lenom Formateur
            //1)Récupération de la session de formation correspondante au user connected 
            List<SessionDeFormation> SessionDeFormations = userconnected.GetSessionDeFormations();
            //2)récupération de cursus de userconnected 
            List<SessionDeCursus> sessionDeCursus = userconnected.GetSessionDeCursus();

            
            EmploiDuTempsDTO emploi = new EmploiDuTempsDTO(userconnected);
            List<JourneeDTO> Mesformations = emploi.ListDates;
            DateTime aujourdhui = DateTime.Now;

            foreach (JourneeDTO j in Mesformations)
            {
                if (j.Date.DayOfYear == DateTime.Now.DayOfYear && j.Date.Year == DateTime.Now.Year)
                {
                    foreach (SessionDeFormation SessionForm in SessionDeFormations)
                    {

                        //DateTraitement = SessionForm.DateDebut;
                        //si la date de début de formation est lundi ou Mardi de la semaine courante je peux afficher 
                        //tout les coordonné concernant l'utilisateur connecté 
                        //if (DateTraitement == FirstDayOfWeek || DateTraitement == FirstDayOfWeek.AddDays(1))


                        //if (aujourdhui >= SessionForm.DateDebut && aujourdhui <= (SessionForm.DateDebut).AddDays(SessionForm.Formation.Dure))
                        //{
                        if (j.Formation.FormationId == SessionForm.Formation.FormationId && j.Formateur.FormateurId == SessionForm.Formateur.FormateurId)
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




            var q = new ViewAsPdf("feuillePresence",f);
            return q;
        }


        public ActionResult feuilleEvaluation()
        {

            string NomFormateur;
            string PrenomFormateur; 
            DateTime DateTraitement = new DateTime();
            DateTime FirstDayOfWeek = new DateTime();
            FicheEval eval = new FicheEval(); 
            //DateTime dt = new DateTime();
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            FirstDayOfWeek = DateTime.Now.AddDays(-diff).Date;
            FirstDayOfWeek.ToString("dd/mm/yyyy");
            ViewData["date3"] = FirstDayOfWeek.ToString("dd/MM/yyyy");

            DateTime LastDayOfWeek = FirstDayOfWeek.AddDays(6);
            ViewData["date4"] = LastDayOfWeek.ToString("dd/MM/yyyy");
            ViewData["date5"] = NomFormation;

            //je vais récupérer le nom de la formation 


            List<SessionDeFormation> SessionDeFormations = userconnected.GetSessionDeFormations();
            //2)récupération de cursus de userconnected 
            List<SessionDeCursus> sessionDeCursus = userconnected.GetSessionDeCursus();
            foreach (SessionDeFormation SessionForm in SessionDeFormations)
            {
                //DateTime aujourdhui = DateTime.Now;
                //DateTraitement = SessionForm.DateDebut;
                //if (aujourdhui <= FirstDayOfWeek && aujourdhui <= LastDayOfWeek)
                //if (DateTraitement == FirstDayOfWeek || DateTraitement == FirstDayOfWeek.AddDays(1))

                EmploiDuTempsDTO emploi = new EmploiDuTempsDTO(userconnected);
                List<JourneeDTO> MesFormation = new List<JourneeDTO>();
                MesFormation = emploi.ListDates;
                DateTime aujourdhui = DateTime.Now;
                if (aujourdhui >= SessionForm.DateDebut && aujourdhui <= (SessionForm.DateDebut).AddDays(SessionForm.Formation.Dure))
                {
                    NomFormateur = SessionForm.Formateur.Nom;
                    ViewBag.Message6 = NomFormateur;
                    
                    PrenomFormateur = SessionForm.Formateur.Prenom;
                    ViewBag.Message7 = PrenomFormateur;
                    //NomFormation = SessionForm.Formation.Nom;
                    //ViewBag.Message5 = NomFormation;
                    eval.vb= SessionForm.Formation.Nom;
                    ViewBag.Message5 = eval.vb;

                    //foreach (SessionDeCursus cursus in sessionDeCursus)
                    //{
                    //    NomCursus = cursus.Cursus.Nom;
                    //    ViewBag.Message3 = NomCursus;
                    //}
                }
                ViewBag.Message8 = userconnected.Nom;
                ViewBag.Message9 = userconnected.Prenom;
                ViewBag.Message10 = userconnected.Email; 



            }
            return View(new FicheEval());
        }
        //[HttpPost]
        //public ActionResult feuilleEvaluation(FicheEval f)
        //{

        //    return new ActionAsPdf("feuilleEvaluation");

        //}
        [HttpPost]

        public ActionResult PrintEval(FicheEval f,string NomFormation)
        {

            string NomFormateur;
            string PrenomFormateur;
            DateTime DateTraitement = new DateTime();
            DateTime FirstDayOfWeek = new DateTime();
            FicheEval eval = new FicheEval();
            //DateTime dt = new DateTime();
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            FirstDayOfWeek = DateTime.Now.AddDays(-diff).Date;
            FirstDayOfWeek.ToString("dd/mm/yyyy");
            ViewData["date3"] = FirstDayOfWeek.ToString("dd/MM/yyyy");

            DateTime LastDayOfWeek = FirstDayOfWeek.AddDays(6);
            ViewData["date4"] = LastDayOfWeek.ToString("dd/MM/yyyy");
            ViewData["date5"] = NomFormation;

            //je vais récupérer le nom de la formation 


            List<SessionDeFormation> SessionDeFormations = userconnected.GetSessionDeFormations();
            //2)récupération de cursus de userconnected 
            List<SessionDeCursus> sessionDeCursus = userconnected.GetSessionDeCursus();
            foreach (SessionDeFormation SessionForm in SessionDeFormations)
            {
                //DateTime aujourdhui = DateTime.Now;
                //DateTraitement = SessionForm.DateDebut;
                //if (aujourdhui <= FirstDayOfWeek && aujourdhui <= LastDayOfWeek)
                //if (DateTraitement == FirstDayOfWeek || DateTraitement == FirstDayOfWeek.AddDays(1))
                DateTime aujourdhui = DateTime.Now;
                if (aujourdhui >= SessionForm.DateDebut && aujourdhui <= (SessionForm.DateDebut).AddDays(SessionForm.Formation.Dure))
                {
                    NomFormateur = SessionForm.Formateur.Nom;
                    ViewBag.Message6 = NomFormateur;

                    PrenomFormateur = SessionForm.Formateur.Prenom;
                    ViewBag.Message7 = PrenomFormateur;
                    //NomFormation = SessionForm.Formation.Nom;
                    //ViewBag.Message5 = NomFormation;

                    ViewBag.Message5 = SessionForm.Formation.Nom;
                    ViewBag.Message8 = userconnected.Nom;
                    ViewBag.Message9 = userconnected.Prenom;
                    ViewBag.Message10 = userconnected.Email;
                }

                    //foreach (SessionDeCursus cursus in sessionDeCursus)
                    //{
                    //    NomCursus = cursus.Cursus.Nom;
                    //    ViewBag.Message3 = NomCursus;
                    //}
                  
            }

            

           



                //f.vb = NomFormation; 
            //ici je dois renvoyer la lfeuille d'evaluation rempli
            return new ViewAsPdf("feuilleEvaluation", f);
        }



    }
}

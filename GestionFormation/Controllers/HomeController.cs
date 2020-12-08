using GestionFormation.DTO;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            if (TempData["emailAEnvoyerDTO"] !=null) { 
            ViewBag.emailAEnvoyerDTO = (EmailAEnvoyerDTO)TempData["emailAEnvoyerDTO"];
                }
            
            if (TempData["emailMessage"] !=null) {
                ViewBag.EmailMessage = TempData["emailMessage"];
            }

            


            return View();
        }

        public ActionResult pagecnxadmin()
        {
            

            return View();
        }

        public ActionResult pagecnxapprenant()
        {


            return View();
        }

        public ActionResult pagecnxformat()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
using GestionFormation.DTO;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Envoyer(EmailAEnvoyerDTO emailAEnvoyerDTO)
        {
            EmailService.EnvoyerEmail(emailAEnvoyerDTO);
            return RedirectToAction("Index", "Home");
        }

    }
}
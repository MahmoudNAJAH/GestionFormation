using GestionFormation.DTO;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Envoyer(EmailAEnvoyerDTO emailAEnvoyerDTO, HttpPostedFileBase FichierAEnvoyer)
        {

            TempData["emailMessage"] = EmailService.Valider(FichierAEnvoyer);
            if (TempData["emailMessage"] == Properties.Resource.FichierTropVolumineux)
            {
                return RedirectToAction("Index", "Home");

            }
            if (TempData["emailMessage"] == Properties.Resource.FichierEnvoye)
            {
                //chemin de destination du fichier
                string chemin = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(FichierAEnvoyer.FileName));
                //sauvegarde sur le serveur
                FichierAEnvoyer.SaveAs(chemin);

                emailAEnvoyerDTO.AttachementPath = EmailService.StockageTransitoire(chemin, FichierAEnvoyer);
            }
            //TempData["emailAEnvoyerDTO"] = emailAEnvoyerDTO;
            EmailService.EnvoyerEmail(emailAEnvoyerDTO);
            if (emailAEnvoyerDTO.AttachementPath != null) System.IO.File.Delete(emailAEnvoyerDTO.AttachementPath);

            return RedirectToAction("Index", "Home");
        }
    }
}

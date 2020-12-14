using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Models;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class PartageFichierController : Controller
    {
        // GET: PartageFichier
        public ActionResult Index(string dirPath = null)
        {
            //Model pour notre View()
            PartageFichierModel model = new PartageFichierModel();
            model.listSessionCursus = new List<SessionDeCursus_PartageFichier_Model>();

            //L'utilisateur devra choisir la SessionDeCursus
            List<SessionDeCursus> listSessionDeCursus = ((UserDTO)Session["userConnected"]).GetSessionDeCursus();

            foreach (SessionDeCursus ses in listSessionDeCursus)
            {
                model.listSessionCursus.Add(new SessionDeCursus_PartageFichier_Model
                {
                    Id = ses.SessionDeCursusId.ToString(),
                    Nom = ses.Cursus.Nom,
                    Description = ses.Cursus.Description,
                }); ;
            }

            if (dirPath != null) model.DirectoryModel = PartageFichierService.GetDirectoryModel(dirPath);

            //Dans le cas ou une seul SessionDeCursus, on pourrait rediriger dès maintenant vers le display du dossier

            return View(model);
        }


        [HttpPost]
        public ActionResult Details(string dirPath)
        {
            string combinedPath = PartageFichierService.GetFullPath(dirPath);
            //Pour les SessionDeCursus, si le dossier n'existe pas, on le créé
            if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        //On utiise une fonction différente pour faire les calculs ici et non dans le js
        [HttpPost]
        public ActionResult PreviousFolder(string dirPath)
        {
            if (dirPath.Split('\\').Length > 1) //On ne doit pas pouvoir aller dans les dossier d'une autre session de cursus
            {
                string[] table = dirPath.Split('\\');               //Chaque dossier du chemin est dans une case du tableau
                int longueur = table[table.Length - 1].Length;      //Le nombre de caractère dans le dernier dossier
                int longeurDirPath = dirPath.Length;                //Le nombre de caractère dans mon DirPath
                                                                    //On veut le dirPath sans le dernier dossier
                                                                    //on prend du caractère 0 jusqu'au dernier dossier
                dirPath = dirPath.Substring(0, longeurDirPath - longueur); 

                //Si on utilise le bouton à plusieurs reprise,
                //Le dernier caractère est \\
                //=> table[table.Length] == ""  => on ne change plus de folder
                //Donc on enlève les accolades :
                if (dirPath[dirPath.Length - 1] == '\\') dirPath = dirPath.Substring(0, dirPath.Length - 1);

            }

            string combinedPath = PartageFichierService.GetFullPath(dirPath); 
            //Pour les SessionDeCursus, si le dossier n'existe pas, on le créé
            if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        // GET: PartageFichier/Create
        public ActionResult CreateDirectory(string dirPath, string dirName)
        {
            //On créé le dossier ave
            Directory.CreateDirectory(Path.Combine(PartageFichierService.GetFullPath(dirPath), dirName));

            //Même si on a créé le dossier, on reste dans le dossier parent pour l'affichage
            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        [HttpPost]
        public ActionResult CreateFile(string dirPath, HttpPostedFileBase myFile)
        {
            if(myFile != null)
            {
                string combinedPath = Path.Combine(PartageFichierService.GetFullPath(dirPath), Path.GetFileName(myFile.FileName));

                // sauvegarde sur le serveur
                myFile.SaveAs(combinedPath);
            }

            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        public ActionResult DeleteDirectory(string dirPath, string dirName)
        {
            PartageFichierService.DeleteFolder(Path.Combine(PartageFichierService.GetFullPath(dirPath), dirName));

            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        public ActionResult DeleteFFile(string dirPath, string fileName)
        {
            System.IO.File.Delete(Path.Combine(PartageFichierService.GetFullPath(dirPath), fileName));

            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(PartageFichierService.GetFullPath(fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}

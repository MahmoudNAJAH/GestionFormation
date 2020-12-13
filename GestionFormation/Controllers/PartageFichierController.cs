using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Models;
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

            if (dirPath != null) model.DirectoryModel = GetDirectoryModel(dirPath);

            //Dans le cas ou une seul SessionDeCursus, on pourrait rediriger dès maintenant vers le display du dossier

            return View(model);
        }


        [HttpPost]
        public ActionResult Details(string dirPath)
        {
            string combinedPath = GetFullPath(dirPath);
            //Pour les SessionDeCursus, si le dossier n'existe pas, on le créé
            if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

            return PartialView("_Details", GetDirectoryModel(dirPath));
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
                dirPath = dirPath.Substring(0, longeurDirPath - longueur); //le -2 pour enlever les accolades

                //Si on utilise le bouton à plusieurs reprise,
                //Le dernier caractère est \\
                //=> table[table.Length] == ""  => on ne change plus de folder
                if (dirPath[dirPath.Length - 1] == '\\') dirPath = dirPath.Substring(0, dirPath.Length - 1);

            }

            string combinedPath = GetFullPath(dirPath); 
            //Pour les SessionDeCursus, si le dossier n'existe pas, on le créé
            if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

            return PartialView("_Details", GetDirectoryModel(dirPath));
        }

        // GET: PartageFichier/Create
        public ActionResult CreateDirectory(string dirPath, string dirName)
        {
            //On créé le dossier ave
            Directory.CreateDirectory(Path.Combine(GetFullPath(dirPath), dirName));

            //Même si on a créé le dossier, on reste dans le dossier parent pour l'affichage
            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        [HttpPost]
        public ActionResult CreateFile(string dirPath, HttpPostedFileBase myFile)
        {
            if(myFile != null)
            {
                string combinedPath = Path.Combine(GetFullPath(dirPath), Path.GetFileName(myFile.FileName));

                // sauvegarde sur le serveur
                myFile.SaveAs(combinedPath);
            }

            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(GetFullPath(fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private string GetFullPath(string dir)
        {
            //Pour permettre l'écriture de fichier en toute sécurité
            //Le chemin vers le dossier ~/Saves est décrit dans le Web.config
            string relativePath = Path.Combine(ConfigurationManager.AppSettings["filePath"], dir);
            //Pour éviter certains soucis de chemin
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            return combinedPath;
        }

        private DirectoryModel GetDirectoryModel(string dirPath)
        {
            DirectoryModel result = new DirectoryModel();

            string combinedPath = GetFullPath(dirPath);

            result.DirPath = dirPath;
            foreach (string str in Directory.EnumerateDirectories(combinedPath)) result.Directories.Add(str.Split('\\')[str.Split('\\').Length - 1]);
            foreach (string str in Directory.EnumerateFiles(combinedPath)) result.Files.Add(str.Split('\\')[str.Split('\\').Length - 1]);

            return result;
        }

    }
}

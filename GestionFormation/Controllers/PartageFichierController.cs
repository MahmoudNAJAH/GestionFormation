using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Filters;
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
    [LoginRequiredFilter]
    public class PartageFichierController : Controller
    {
        /// <summary>
        /// Page de Base avec deux partie : 
        ///     - choix de la sesion de Cursus
        ///     - Partial("Détail") avec tous le contenu
        ///     
        /// On calcul le model qui contient la liste des fichiers et dossiers contenu dans le dossier passer en paramètre
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public ActionResult Index(string dirPath = null)
        {
            //Model pour notre View()
            PartageFichierModel model = new PartageFichierModel();
            model.listSessionCursus = new List<SessionDeCursus_PartageFichier_Model>();

            //L'utilisateur devra choisir la SessionDeCursus
            List<SessionDeCursus> listSessionDeCursus = ((UserDTO)Session["userConnected"]).GetSessionDeCursus();

            //On récupère les informations de base de toutes les sessions de cursus
            foreach (SessionDeCursus ses in listSessionDeCursus)
            {
                model.listSessionCursus.Add(new SessionDeCursus_PartageFichier_Model
                {
                    Id = ses.SessionDeCursusId.ToString(),
                    Nom = ses.Cursus.Nom,
                    Description = ses.Cursus.Description,
                }); 
            }

            //On génère le model contenant les fichiers
            if (dirPath != null) model.DirectoryModel = PartageFichierService.GetDirectoryModel(dirPath);

            //Dans le cas ou une seul SessionDeCursus, on pourrait rediriger dès maintenant vers le display du dossier

            return View(model);
        }

        [HttpPost]
        /// <summary>
        /// Pour la vue Partielle contenant la "datagrid"
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public ActionResult Details(string dirPath)
        {
            //On récupère le chemin absolu.
            //Le paramètre ici n'st que le nom du dossier, non son fullPath
            string combinedPath = PartageFichierService.GetFullPath(dirPath);

            //Si le dossier n'existe pas, on le créé
            //Utile quand on vient juste de choisir la sessiondecursus, tous les dossiers ne sont pas créé
            if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        /// <summary>
        /// Utilisé avec le bouton "<-" dans #Détails
        /// Permet de retourner dans le dossier parent
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PreviousFolder(string dirPath)
        {
            //On ne doit pas pouvoir aller dans les dossier d'une autre session de cursus
            //Si on est dans le dossier de la sessiondecursus, on ne fait pas les changements pour aller dans le dossier parent
            if (dirPath.Split('\\').Length > 1) 
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

            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        [HttpPost]
        public ActionResult CreateDirectory(string dirPath, string dirName)
        {
            //On créé le dossier
            Directory.CreateDirectory(Path.Combine(PartageFichierService.GetFullPath(dirPath), dirName));

            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        [HttpPost]
        public ActionResult CreateFile(string dirPath, HttpPostedFileBase myFile)
        {
            //Pour bloquer le bouton si on a pas sélectionné de fichier
            if(myFile != null)
            {
                string combinedPath = Path.Combine(PartageFichierService.GetFullPath(dirPath), Path.GetFileName(myFile.FileName));
                // sauvegarde sur le serveur
                myFile.SaveAs(combinedPath);
            }

            return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
        }

        [HttpPost]
        public ActionResult DeleteDirectory(string dirPath, string dirName)
        {
            PartageFichierService.DeleteFolder(Path.Combine(PartageFichierService.GetFullPath(dirPath), dirName));

            //return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        [HttpPost]
        public ActionResult DeleteFile(string dirPath, string fileName)
        {
            System.IO.File.Delete(Path.Combine(PartageFichierService.GetFullPath(dirPath), fileName));

            //return RedirectToAction("Index", "PartageFichier", new { dirPath = dirPath });
            return PartialView("_Details", PartageFichierService.GetDirectoryModel(dirPath));
        }

        /// <summary>
        /// Télécharge le fichier quand on click dessus
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(PartageFichierService.GetFullPath(fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}

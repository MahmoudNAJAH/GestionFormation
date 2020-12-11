﻿using GestionFormation.DTO;
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

            DirectoryModel model = GetDirectoryModel(dirPath);

            return PartialView("_Details", model);
        }

        // GET: PartageFichier/Create
        public ActionResult CreateDirectory(string dirPath, string dirName)
        {
            //On récupère le path du dossier parent
            string combinedPath = GetFullPath(dirPath);

            //On créé le dossier ave
            Directory.CreateDirectory(Path.Combine(combinedPath, dirName));

            //Même si on a créé le dossier, on reste dans le dossier parent pour l'affichage
            return RedirectToAction("Index", "PartageFichier", new { dirPath = combinedPath });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateFile(string dirPath, HttpPostedFileBase myFile)
        {
            string combinedPath = Path.Combine(GetFullPath(dirPath), Path.GetFileName(myFile.FileName));

            // sauvegarde sur le serveur
            myFile.SaveAs(combinedPath);

            return RedirectToAction("Index", "PartageFichier", new { dirPath = GetFullPath(dirPath) });
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

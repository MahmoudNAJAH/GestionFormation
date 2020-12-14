using GestionFormation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class PartageFichierService
    {
        public static string GetFullPath(string dir)
        {
            //Pour permettre l'écriture de fichier en toute sécurité
            //Le chemin vers le dossier ~/Saves est décrit dans le Web.config
            string relativePath = Path.Combine(ConfigurationManager.AppSettings["filePath"], dir);
            //Pour éviter certains soucis de chemin
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            return combinedPath;
        }

        public static DirectoryModel GetDirectoryModel(string dirPath)
        {
            DirectoryModel result = new DirectoryModel();

            string combinedPath = GetFullPath(dirPath);

            result.DirPath = dirPath;
            foreach (string str in Directory.EnumerateDirectories(combinedPath)) result.Directories.Add(str.Split('\\')[str.Split('\\').Length - 1]);
            foreach (string str in Directory.EnumerateFiles(combinedPath)) result.Files.Add(str.Split('\\')[str.Split('\\').Length - 1]);

            return result;
        }

        public static void DeleteFolder(string dirPath)
        {
            //On efface les ficheir
            foreach (string str in Directory.EnumerateFiles(dirPath))
                System.IO.File.Delete(str);

            //On efface les dossiers
            foreach (string str in Directory.EnumerateDirectories(dirPath))
            {
                DeleteFolder(str);
                Directory.Delete(str);
            }
        }
    }
}
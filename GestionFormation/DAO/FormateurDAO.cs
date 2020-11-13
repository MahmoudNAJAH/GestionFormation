using GestionFormation.DAO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestionformation.DAO
{
    public class FormateurDAO
    {
        public static void Create(Formateur fr)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Formateurs.Add(fr);

                context.SaveChanges();
            }
        }
        public static Formateur FindById(int FormateurId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Formateurs.Include("SessionDeFormations").FirstOrDefault(fr => fr.FormateurId == FormateurId);
            }
        }

        public static List<Formateur> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Formateurs.Include("SessionDeFormations").ToList();
            }
        }

        public static void Update(Formateur fr)
        {
            using (BDDContext context = new BDDContext())
            {
                Formateur frDansDB = FindById(fr.FormateurId);
                if (fr.Nom != null) frDansDB.Nom = fr.Nom;
                if (fr.Prenom != null) frDansDB.Prenom = fr.Prenom;
                if (fr.Email != null) frDansDB.Email = fr.Email;
                if (fr.SessionDeFormations != null) frDansDB.SessionDeFormations = fr.SessionDeFormations;               

                context.SaveChanges();
            }
        }

        public static void Delete(Formateur fr)
        {
            using (BDDContext context = new BDDContext())
            {

                context.Formateurs.Remove(fr);
                context.SaveChanges();

            }
        }
    }
}
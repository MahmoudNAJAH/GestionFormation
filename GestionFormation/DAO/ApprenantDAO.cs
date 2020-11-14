using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GestionFormation.DAO
{
    public class ApprenantDAO
    {
        public static void Create(Apprenant ap)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Apprenants.Add(ap);
                context.SaveChanges();
            }
        }
        public static Apprenant FindById(int apprenantId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Apprenants.Include("Messages").Include("SessionDeCursus").FirstOrDefault(ap => ap.ApprenantId == apprenantId);
            }
        }

        public static List<Apprenant> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Apprenants.Include("Messages").Include("SessionDeCursus").ToList();
            }
        }

        public static void Update(Apprenant ap)
        {
            using (BDDContext context = new BDDContext())
            {
                Apprenant apDansDB = FindById(ap.ApprenantId);
                if (ap.Nom != null) apDansDB.Nom = ap.Nom;
                if (ap.Prenom != null) apDansDB.Prenom = ap.Prenom;
                if (ap.Email != null) apDansDB.Email = ap.Email;
                if (ap.MotDePasse != null) apDansDB.MotDePasse = ap.MotDePasse;

                if (ap.Messages != null) apDansDB.Messages = ap.Messages;
                if (ap.SessionDeCursus != null) apDansDB.SessionDeCursus = ap.SessionDeCursus;

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Apprenants.Remove(context.Apprenants.FirstOrDefault(app => app.ApprenantId == id));
                context.SaveChanges();
            }
        }
    }
}
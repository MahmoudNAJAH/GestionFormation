using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class SessionDeFormationDAO
    {
        public static void Create(SessionDeFormation sdf)
        {
            using (BDDContext context = new BDDContext())
            {
                context.SessionDeFormations.Add(sdf);

                context.SaveChanges();
            }
        }
        public static SessionDeFormation FindById(int sessionDeFormationId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeFormations.Include("Formateur").Include("Formation").Include("SessionDeCursus").FirstOrDefault(sdf => sdf.SessionDeFormationId == sessionDeFormationId);
            }
        }

        public static List<SessionDeFormation> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeFormations.Include("Formateur").Include("Formation").Include("SessionDeCursus").ToList();
            }
        }

        public static void Update(SessionDeFormation sdf)
        {
            using (BDDContext context = new BDDContext())
            {
                SessionDeFormation sdfDansDB = context.SessionDeFormations.Include("Formateur").Include("Formation").Include("SessionDeCursus").FirstOrDefault(s => s.SessionDeFormationId == sdf.SessionDeFormationId);
                if (sdf.DateDebut != null) sdfDansDB.DateDebut = sdf.DateDebut;
                if (sdf.Formateur != null) sdfDansDB.Formateur = sdf.Formateur;
                if (sdf.Formation != null) sdfDansDB.Formation = sdf.Formation;
                if (sdf.SessionDeCursus != null) sdfDansDB.SessionDeCursus = sdf.SessionDeCursus;                

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {

                context.SessionDeFormations.Remove(context.SessionDeFormations.Include("Formateur").Include("Formation").Include("SessionDeCursus").FirstOrDefault(s => s.SessionDeFormationId == id));
                context.SaveChanges();

            }
        }
    }
}
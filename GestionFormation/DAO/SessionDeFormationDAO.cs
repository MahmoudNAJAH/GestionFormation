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
                context.SessionDeFormation.Add(sdf);

                context.SaveChanges();
            }
        }
        public static SessionDeFormation FindById(int sessionDeFormationId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeFormation.Include("Formateur").Include("Formation").Include("SessionDeCursus").FirstOrDefault(sdf => sdf.SessionDeFormationId == sessionDeFormationId);
            }
        }

        public static List<SessionDeFormation> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeFormation.Include("Formateur").Include("Formation").Include("SessionDeCursus").ToList();
            }
        }

        public static void Update(SessionDeFormation sdf)
        {
            using (BDDContext context = new BDDContext())
            {
                SessionDeFormation sdfDansDB = FindById(sdf.SessionDeFormationId);
                if (sdf.Formateur != null) sdfDansDB.Formateur = sdf.Formateur;
                if (sdf.Formation != null) sdfDansDB.Formation = sdf.Formation;
                if (sdf.SessionDeCursus != null) sdfDansDB.SessionDeCursus = sdf.SessionDeCursus;                

                context.SaveChanges();
            }
        }

        public static void Delete(SessionDeFormation sdf)
        {
            using (BDDContext context = new BDDContext())
            {

                context.SessionDeFormation.Remove(sdf);
                context.SaveChanges();

            }
        }
    }
}
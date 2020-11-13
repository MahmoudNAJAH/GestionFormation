using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class SessionDeCursusDAO
    {
        public static void Create(SessionDeCursus sdc)
        {
            using (BDDContext context = new BDDContext())
            {
                context.SessionDeCursus.Add(sdc);

                context.SaveChanges();
            }
        }
        public static SessionDeCursus FindById(int sessionDeCursusId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeCursus.Include("Apprenants").Include("SessionsDeFormations").FirstOrDefault(sdc => sdc.SessionDeCursusId == sessionDeCursusId);
            }
        }

        public static List<SessionDeCursus> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeCursus.Include("Apprenants").Include("SessionsDeFormations").ToList();
            }
        }

        public static void Update(SessionDeCursus sdc)
        {
            using (BDDContext context = new BDDContext())
            {
                SessionDeCursus sdcDansDB = FindById(sdc.SessionDeCursusId);
                if (sdc.Apprenants != null) sdcDansDB.Apprenants = sdc.Apprenants;
                if (sdc.Cursus != null) sdcDansDB.Cursus = sdc.Cursus;
                if (sdc.SessionsDeFormations != null) sdcDansDB.SessionsDeFormations = sdc.SessionsDeFormations;                

                context.SaveChanges();
            }
        }

        public static void Delete(SessionDeCursus sdc)
        {
            using (BDDContext context = new BDDContext())
            {

                context.SessionDeCursus.Remove(sdc);
                context.SaveChanges();

            }
        }
    }
}
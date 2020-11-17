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
                return context.SessionDeCursus.Include("Cursus").Include("Apprenants").Include("SessionsDeFormations").FirstOrDefault(sdc => sdc.SessionDeCursusId == sessionDeCursusId);
            }
        }

        public static List<SessionDeCursus> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.SessionDeCursus.Include("Cursus").Include("Apprenants").Include("SessionsDeFormations").ToList();
            }
        }

        public static void Update(SessionDeCursus sdc)
        {
            using (BDDContext context = new BDDContext())
            {
                SessionDeCursus sdcDansDB = context.SessionDeCursus.Include("Cursus").Include("Apprenants").Include("SessionsDeFormations").FirstOrDefault(s => s.SessionDeCursusId == sdc.SessionDeCursusId);
                
                //Foreign keys
                if (sdc.Cursus != null) sdcDansDB.Cursus = context.Cursus.FirstOrDefault(c => c.CursusId == sdc.Cursus.CursusId);
                if (sdc.Apprenants != null)
                {
                    sdcDansDB.Apprenants = new List<Apprenant>();
                    foreach (Apprenant app in sdc.Apprenants)
                        sdcDansDB.Apprenants.Add(context.Apprenants.FirstOrDefault(c => c.ApprenantId == app.ApprenantId));
                }
                if (sdc.SessionsDeFormations != null)
                {
                    sdcDansDB.SessionsDeFormations = new List<SessionDeFormation>();
                    foreach (SessionDeFormation ses in sdc.SessionsDeFormations)
                        sdcDansDB.SessionsDeFormations.Add(context.SessionDeFormations.FirstOrDefault(c => c.SessionDeFormationId == ses.SessionDeFormationId));
                }

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {

                context.SessionDeCursus.Remove(context.SessionDeCursus.FirstOrDefault(sdc => sdc.SessionDeCursusId == id));
                context.SaveChanges();

            }
        }
    }
}
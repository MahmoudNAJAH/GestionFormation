using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class FormationDAO
    {
        public static void Create(Formation fn)
        {
            using (BDDContext context = new BDDContext())
            {
                
                if (fn.Cursus != null)
                {
                    List<Cursus> listCursus = new List<Cursus>();
                    foreach (Cursus curs in fn.Cursus)
                        //Pas de contrainte de Multiplicité ici : List des deux côtés
                        listCursus.Add(context.Cursus.FirstOrDefault(c => c.CursusId == curs.CursusId));
                    fn.Cursus = listCursus;
                }
                if (fn.SessionsDeFormations != null)
                {
                    List<SessionDeFormation> listSessionDeFormation = new List<SessionDeFormation>();
                    foreach (SessionDeFormation ses in fn.SessionsDeFormations)
                        listSessionDeFormation.Add(context.SessionDeFormations.FirstOrDefault(s => s.SessionDeFormationId == ses.SessionDeFormationId));
                    fn.SessionsDeFormations = listSessionDeFormation;
                }
                

                context.Formations.Add(fn);
                context.SaveChanges();
            }
        }

        public static Formation FindById(int formationId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Formations.Include("Cursus").Include("SessionsDeFormations").FirstOrDefault(fn => fn.FormationId == formationId);
            }
        }

        public static List<Formation> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Formations.Include("Cursus").Include("SessionsDeFormations").ToList();
            }
        }

        public static void Update(Formation fn)
        {
            using (BDDContext context = new BDDContext())
            {
                Formation fnDansDB = context.Formations.Include("Cursus").Include("SessionsDeFormations").FirstOrDefault(f => f.FormationId == fn.FormationId);
                if (fn.Nom != null) fnDansDB.Nom = fn.Nom;
                if (fn.Description != null) fnDansDB.Description = fn.Description;
                if (fn?.Dure != null) fnDansDB.Dure = fn.Dure;

                //Foreign keys
                if (fn.Cursus != null)
                {
                    fnDansDB.Cursus = new List<Cursus>();
                    foreach(Cursus curs in fn.Cursus)
                        fnDansDB.Cursus.Add(context.Cursus.FirstOrDefault(c => c.CursusId == curs.CursusId));

                }
                if (fn.SessionsDeFormations != null)
                {
                    fnDansDB.SessionsDeFormations = new List<SessionDeFormation>();
                    foreach (SessionDeFormation curs in fn.SessionsDeFormations)
                        fnDansDB.SessionsDeFormations.Add(context.SessionDeFormations.FirstOrDefault(c => c.SessionDeFormationId == curs.SessionDeFormationId));
                }
                context.SaveChanges();
            }
        }

        

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Formations.Remove(context.Formations.FirstOrDefault(f => f.FormationId == id));
                context.SaveChanges();
            }
        }
    }
}
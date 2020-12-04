using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class CursusDAO
    {
        public static void Create(Cursus c)
        {
            using (BDDContext context = new BDDContext())
            {
                if (c.Formations != null)
                {
                    List<Formation> listFormation = new List<Formation>();
                    foreach (Formation curs in c.Formations)
                        //Pas de contrainte de Multiplicité ici : List des deux côtés
                        listFormation.Add(context.Formations.FirstOrDefault(cu => cu.FormationId == curs.FormationId));
                    c.Formations = listFormation;
                }
                if (c.SessionDeCursus != null)
                {
                    List<SessionDeCursus> listSessionDeCursus = new List<SessionDeCursus>();
                    foreach (SessionDeCursus ses in c.SessionDeCursus)
                        listSessionDeCursus.Add(context.SessionDeCursus.FirstOrDefault(s => s.SessionDeCursusId == ses.SessionDeCursusId));
                    c.SessionDeCursus = listSessionDeCursus;
                }

                context.Cursus.Add(c);

                context.SaveChanges();
            }
        }
        public static Cursus FindById(int cursusId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Cursus.Include("Formations").Include("SessionDeCursus").FirstOrDefault(c => c.CursusId == cursusId);
            }
        }

        public static List<Cursus> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Cursus.Include("Formations").Include("SessionDeCursus").ToList();
            }
        }

        public static void Update(Cursus c)
        {
            using (BDDContext context = new BDDContext())
            {
                Cursus cDansDB = context.Cursus.Include("Formations").Include("SessionDeCursus").FirstOrDefault(Curs => Curs.CursusId == c.CursusId);
                if (c.Nom != null) cDansDB.Nom = c.Nom;
                if (c.Description != null) cDansDB.Description = c.Description;

                //foreign keys
                if (c.Formations != null)
                {
                    cDansDB.Formations = new List<Formation>();
                    foreach(Formation form in c.Formations)
                        cDansDB.Formations.Add(context.Formations.FirstOrDefault(f => f.FormationId == form.FormationId));
                }
                if (c.SessionDeCursus != null)
                {
                    cDansDB.SessionDeCursus = new List<SessionDeCursus>();
                    foreach (SessionDeCursus form in c.SessionDeCursus)
                        cDansDB.SessionDeCursus.Add(context.SessionDeCursus.FirstOrDefault(f => f.SessionDeCursusId == form.SessionDeCursusId));
                }

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Cursus.Remove(context.Cursus.FirstOrDefault(Curs => Curs.CursusId == id));
                context.SaveChanges();
            }
        }
    }
}
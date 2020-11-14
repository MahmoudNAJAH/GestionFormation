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
                if (c.Formations != null) cDansDB.Formations = c.Formations;
                if (c.SessionDeCursus != null) cDansDB.SessionDeCursus = c.SessionDeCursus;

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
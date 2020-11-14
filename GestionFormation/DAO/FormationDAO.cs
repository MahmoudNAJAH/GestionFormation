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
                if (fn.Cursus != null) fnDansDB.Cursus = fn.Cursus;
                if (fn.SessionsDeFormations != null) fnDansDB.SessionsDeFormations = fn.SessionsDeFormations;               

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
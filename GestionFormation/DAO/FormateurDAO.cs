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
                if(fr.SessionDeFormations != null)
                {
                    List<SessionDeFormation> listSessionDeFormation = new List<SessionDeFormation>();
                    foreach (SessionDeFormation curs in fr.SessionDeFormations)
                        //Pas de contrainte de Multiplicité ici : List des deux côtés
                        listSessionDeFormation.Add(context.SessionDeFormations.FirstOrDefault(c => c.SessionDeFormationId == curs.SessionDeFormationId));
                    fr.SessionDeFormations = listSessionDeFormation;
                }

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

        public static Formateur FindByLgMD(string mail)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Formateurs.FirstOrDefault(a => a.Email == mail);
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
                Formateur frDansDB = context.Formateurs.Include("SessionDeFormations").FirstOrDefault(f => f.FormateurId == fr.FormateurId);
                if (fr.Nom != null) frDansDB.Nom = fr.Nom;
                if (fr.Prenom != null) frDansDB.Prenom = fr.Prenom;
                if (fr.Email != null) frDansDB.Email = fr.Email;
                if (fr.MotDePasse != null) frDansDB.MotDePasse = fr.MotDePasse;

                //Foreign keys
                if (fr.SessionDeFormations != null)
                {
                    frDansDB.SessionDeFormations = new List<SessionDeFormation>();
                    foreach(SessionDeFormation ses in fr.SessionDeFormations)
                        frDansDB.SessionDeFormations.Add(context.SessionDeFormations.FirstOrDefault(s => s.SessionDeFormationId == ses.SessionDeFormationId));
                }

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (BDDContext context = new BDDContext())
            {

                context.Formateurs.Remove(context.Formateurs.FirstOrDefault(f => f.FormateurId == id));
                context.SaveChanges();

            }
        }


        internal static List<Formateur> FindByApprenantId(int id)
        {
            using (BDDContext context = new BDDContext())
            {
                // Recupération des id des formateurs ayant une session de formation dont la session de cursus est suivie par l'apprenant ayant pour id (id)
                // => requete complexe élaborée sous SQL management studio
           
                string query = "Select distinct SessionDeFormations.Formateur_FormateurId from SessionDeFormations " +
                    "join SessionDeCursus on SessionDeCursus.SessionDeCursusId = SessionDeFormations.SessionDeCursus_SessionDeCursusId " +
                    "join SessionDeCursusApprenants on SessionDeCursus.SessionDeCursusId = SessionDeCursusApprenants.SessionDeCursus_SessionDeCursusId " +
                    "where SessionDeCursusApprenants.Apprenant_ApprenantId = {0}";

                List<int> formateursIds = context.Database.SqlQuery<int>(query, id).ToList();


                // Récupération des formateurs à partir de la liste d'ID
                List<Formateur> formateurs = new List<Formateur>();
                foreach (int fId in formateursIds)
                {
                    formateurs.Add(context.Formateurs.Include("SessionDeFormations").FirstOrDefault(fr => fr.FormateurId == fId));
                }

                return formateurs;
            }


        }
    }
}
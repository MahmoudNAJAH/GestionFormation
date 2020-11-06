using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
  public  class StagiaireDAO
    {
        public static void Create(Stagiaire stagiaire )
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Stagiaires.Add(stagiaire);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }


        public static void Createmany(params Stagiaire[] stagiaires)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Stagiaires.AddRange(stagiaires);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static List<Stagiaire> FindAll()
        {

            using (BDDContext context = new BDDContext())
            {
                return context.Stagiaires.ToList();

            }
        }

        public static Stagiaire FindById(int Idstagiaire)
        {
            using(BDDContext context = new BDDContext())
            {
                return context.Stagiaires.FirstOrDefault(S => S.StagiaireId == Idstagiaire);
            }
        }

        public static void Insert(Stagiaire nouveauStagiaire)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Stagiaires.Add(nouveauStagiaire);
                    context.SaveChanges();
                }

            }

            catch (Exception ex)
            {

            }

        }

        public static void Update(Stagiaire stagiaire)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Stagiaire S = context.Stagiaires.FirstOrDefault(S => S.StagiaireId == stagiaire.StagiaireId);
                    S.Nom = stagiaire.Nom;
                    S.Prenom = stagiaire.Prenom;
                    S.Mot_De_Passe = stagiaire.Mot_De_Passe;
                    S.SessionsCursus = stagiaire.SessionsCursus; 
                    S.E_mail = stagiaire.E_mail;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}

using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
  public  class FormationDAO
    {
        public static void Create(Formation formation)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Formations.Add(formation);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static List<Formation> FindAll()
        {

            using (BDDContext context = new BDDContext())
            {
                return context.Formations.ToList();

            }
        }

        public static void Update(Formation formation)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Formation f= context.Formations.FirstOrDefault(s => s.FormationId == formation.FormationId);

                    f.titre = formation.titre;
                    f.Description = formation.Description;
                    f.LesCursus = formation.LesCursus;
                    f.SessionDeFormations = formation.SessionDeFormations;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}

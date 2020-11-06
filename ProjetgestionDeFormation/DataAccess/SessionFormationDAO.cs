using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
    public class SessionFormationDAO
    {
        public static void Create(Session_De_Formation ST)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.SessionFormations.Add(ST);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static List<Session_De_Formation> FindAll()
        {

            using (BDDContext context = new BDDContext())
            {
                return context.SessionFormations.ToList();

            }
        }

        public static void Insert(Session_De_Formation St)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.SessionFormations.Add(St);
                    context.SaveChanges();
                }

            }

            catch (Exception ex)
            {

            }

        }

        public static void Update(Session_De_Formation St)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Session_De_Formation Sf = context.SessionFormations.FirstOrDefault(S =>S.Session_De_FormationId == St.Session_De_FormationId);
                    Sf.Description = St.Description;
                    Sf.Formateurs = St.Formateurs;
                    Sf.SessionCursus = St.SessionCursus;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}

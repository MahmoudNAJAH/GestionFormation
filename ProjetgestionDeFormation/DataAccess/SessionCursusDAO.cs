using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
 public   class SessionCursusDAO
    {
        public static void Create(Session_De_Cursus Sc)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.SessionCursus.Add(Sc);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static List<Session_De_Cursus> FindAll()
        {

            using (BDDContext context = new BDDContext())
            {
                return context.SessionCursus.ToList();

            }
        }

        public static void Insert(Session_De_Cursus Sc)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.SessionCursus.Add(Sc);
                    context.SaveChanges();
                }

            }

            catch (Exception ex)
            {

            }

        }

        public static void Update(Session_De_Cursus Sc)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Session_De_Cursus Sfc = context.SessionCursus.FirstOrDefault(s => s.Session_De_CursusId == Sc.Session_De_CursusId);
                    
                    Sfc.Description = Sc.Description;
                    Sfc.Stagiaires = Sc.Stagiaires;
                    Sfc.SessionFormations = Sc.SessionFormations;
                    
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}

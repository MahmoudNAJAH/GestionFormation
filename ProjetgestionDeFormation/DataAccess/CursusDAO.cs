using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
    public class CursusDAO
    {
        public static void Create(Cursus cursus)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Cursuss.Add(cursus);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static List<Cursus> FindAll()
        {

            using (BDDContext context = new BDDContext())
            {
                return context.Cursuss.ToList();

            }
        }
        public static void Update(Cursus cursus)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Cursus c = context.Cursuss.FirstOrDefault(s => s.CursusId == cursus.CursusId);

                    c.titre = cursus.titre;
                    c.Description = cursus.Description;
                    c.Formations = cursus.Formations;
                    c.CursusSuivi = cursus.CursusSuivi;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

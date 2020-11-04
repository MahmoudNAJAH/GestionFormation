using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation
{
    class Program
    {
        static void Main(string[] args)
        {

            static void Main(string[] args)
            {
                Cursus c = new Cursus();
                using (BDDContext context = new BDDContext())
                {
                    c = new Cursus
                    {
                        titre = "Concepteur Developpeur "
                    };
                    context.Cursuss.Add(c);
                    context.SaveChanges();
                }

                Formation form = new Formation();
                using (BDDContext context = new BDDContext())
                {
                    form = new Formation
                    {
                        titre = "java "
                    };
                    context.Formations.Add(form);
                    context.SaveChanges();
                }

                Stagiaire st = new Stagiaire();
                using (BDDContext context = new BDDContext())
                {
                    st = new Stagiaire
                    {
                        Nom = "Premier stagiaire"
                    };
                    context.Stagiaires.Add(st);
                    context.SaveChanges();
                }

                Formateur f = new Formateur();
                using (BDDContext context = new BDDContext())
                {
                    f = new Formateur
                    {
                        Nom = "Christopher"
                    };
                    context.Formateurs.Add(f);
                    context.SaveChanges();
                }

                Session_De_Cursus Sc = new Session_De_Cursus();
                using (BDDContext context = new BDDContext())
                {
                    Sc = new Session_De_Cursus
                    {
                        Description = "Ce cursus  consiste à travailler sur le profil de développeurs web  "
                    };
                    context.SessionCursus.Add(Sc);
                    context.SaveChanges();
                }

                Session_De_Formation Sf = new Session_De_Formation();

                using (BDDContext context = new BDDContext())
                {
                    Sf = new Session_De_Formation
                    {
                        Description = "Cette formation consiste à former  "
                    };
                    context.SessionFormations.Add(Sf);
                    context.SaveChanges();
                }
            }
        }
    }
}

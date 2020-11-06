using Projetdawan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetgestionDeFormation.DataAccess
{
 public   class FormateurDAO
    {


        public static void Create(Formateur formateur )
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Formateurs.Add(formateur);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        public static void Createmany(params Formateur[] formateurs)
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Formateurs.AddRange(formateurs);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            
        }
        public static List<Formateur> FindAll()
        {




            using (BDDContext context = new BDDContext())
            {
                return context.Formateurs.ToList();



            }
        }


            
        public static void Insert(Formateur nouveauformateur)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {
                    context.Formateurs.Add(nouveauformateur);
                    context.SaveChanges();
                }
                    
            }
                   
             catch(Exception ex)
                {
                    
                }
           
        }

        public static void Update(Formateur formateur)
        {

            try
            {
                using (BDDContext context = new BDDContext())
                {

                    Formateur f = context.Formateurs.FirstOrDefault(f => f.FormateurId == formateur.FormateurId);
                    f.Nom = formateur.Nom;
                    f.Prenom = formateur.Prenom;
                    f.Mot_De_Passe = formateur.Mot_De_Passe;
                    f.SessionsFormations = formateur.SessionsFormations;
                    f.E_mail = formateur.E_mail;
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        public static void Delete(int id )
        {
            try
            {
                using (BDDContext context = new BDDContext())
                {

                    context.Formateurs.Remove(context.Formateurs.FirstOrDefault(f => f.FormateurId == id));

                    context.SaveChanges();

                }
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}

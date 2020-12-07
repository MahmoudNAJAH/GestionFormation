using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionFormation.WebServices
{
    public class ApprenantAPIController : ApiController
    {
        // GET: api/ApprenantAPI
       
     
        public List<Apprenant> Get()
        {
            return ApprenantDAO.FindAll();
        }


        // GET: api/ApprenantAPI/5
        public Apprenant Get(int id)
        {
            return ApprenantDAO.FindById(id);
        }

        // POST: api/ApprenantAPI
        //ici je vais passer un string 
        public void   Post([FromBody] UserDTO2 ap )
        {
            Apprenant ap1 = new Apprenant();
            ap1.ApprenantId = ap.Id;
            ap1.Email = ap.Email;
            ap1.Nom = ap.Nom;
            ap1.Prenom = ap.Prenom;
            ap1.SessionDeCursus = ap.SessionDeCursus;
            ap1.Messages = ap.Messages;

            //maintenant j'ai transformé mon mot de passe qui est entré en string en tableau de byte 



            ap1.MotDePasse = UserDTO2.transformeEnBytes(ap.MotDePasse);
            CryptageMotDePasse password = new CryptageMotDePasse(ap.MotDePasse);
            var hexString = BitConverter.ToString(password.ToArray()).Replace("-", "");
            //string chaine = hexString.Replace("0x", "");
            ap1.MotDePasse = System.Text.Encoding.ASCII.GetBytes(hexString);
         
            ApprenantDAO.Create(ap1);
            

        }

        // PUT: api/ApprenantAPI/5
        public void Put(int id, [FromBody] UserDTO2  ap )
        {
            // putt c'est l'appdate


            Apprenant ap1 = new Apprenant();
              ap1.ApprenantId = ap.Id;
            ap1.Nom = ap.Nom;
            ap1.Email = ap.Email;
               
                ap1.Prenom = ap.Prenom;
                ap1.SessionDeCursus = ap.SessionDeCursus;
                ap1.Messages = ap.Messages;

                ap1.MotDePasse = UserDTO2.transformeEnBytes(ap.MotDePasse);
                CryptageMotDePasse password = new CryptageMotDePasse(ap.MotDePasse);
                var hexString = BitConverter.ToString(password.ToArray()).Replace("-", "");
                //string chaine = hexString.Replace("0x", "");
                ap1.MotDePasse = System.Text.Encoding.ASCII.GetBytes(hexString);


            using (BDDContext context =new BDDContext())
            {
                Apprenant apprenant = context.Apprenants.FirstOrDefault(app => app.ApprenantId == id);
                apprenant.Nom = ap1.Nom; 
                apprenant.Email = ap1.Email;
                apprenant.Prenom = ap1.Prenom;
                apprenant.SessionDeCursus = ap1.SessionDeCursus;
                apprenant.Messages = ap1.Messages;
                apprenant.MotDePasse = ap1.MotDePasse;
                context.SaveChanges();


            }

                
    

           

            
             
        }

        // DELETE: api/ApprenantAPI/5
        public void Delete(int id)
        {
            ApprenantDAO.Delete(id); 
        }
    }
}

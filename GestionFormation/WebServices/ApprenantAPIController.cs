using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public  void   Post([FromBody] UserDTOAPI ap )
        {
            Apprenant ap1 = new Apprenant();
            ap1.ApprenantId = ap.Id;
            ap1.Email = ap.Email;
            ap1.Nom = ap.Nom;
            ap1.Prenom = ap.Prenom;
            if(ap.SessionDeCursus != null)
            {
                foreach (SessionDeCursus Cs in ap.SessionDeCursus)
                {
                   List<SessionDeCursus> sessionCursus = new List<SessionDeCursus>();
                    sessionCursus.Add(Cs);
                    ap1.SessionDeCursus = sessionCursus; 
                }
            }
           
               

            ap1.Messages = ap.Messages;

            CryptageMotDePasse password = new CryptageMotDePasse(ap.MotDePasse);
            ap1.MotDePasse = password.ToArray();
            //ApprenantDAO.Create(ap1);



            using (BDDContext context = new BDDContext())
            {
           
                context.Apprenants.Add(ap1);
                context.SaveChanges();
            }


        }

        // PUT: api/ApprenantAPI/5
        public void Put(int id, [FromBody] UserDTOAPI  ap )
        {
            // putt c'est l'appdate


            Apprenant ap1 = new Apprenant();
              ap1.ApprenantId = ap.Id;
               ap1.Nom = ap.Nom;
               ap1.Email = ap.Email;
               
                ap1.Prenom = ap.Prenom;
                ap1.SessionDeCursus = ap.SessionDeCursus;
                ap1.Messages = ap.Messages;
            CryptageMotDePasse password = new CryptageMotDePasse(ap.MotDePasse);
            ap1.MotDePasse = password.ToArray();


            using (BDDContext context = new BDDContext())
            {
                Apprenant apprenant = context.Apprenants.FirstOrDefault(app => app.ApprenantId == id);
                apprenant.Nom = ap1.Nom;
                apprenant.Email = ap1.Email;
                apprenant.Prenom = ap1.Prenom;
                //foreign keys
                if (ap.Messages != null)
                {
                    apprenant.Messages = new List<Message>();
                    foreach (Message mes in ap.Messages)
                        apprenant.Messages.Add(context.Messages.FirstOrDefault(m => m.MessageId == mes.MessageId));
                }
                if (ap.SessionDeCursus != null)
                {
                    apprenant.SessionDeCursus = new List<SessionDeCursus>();
                    foreach (SessionDeCursus mes in ap.SessionDeCursus)
                        apprenant.SessionDeCursus.Add(context.SessionDeCursus.FirstOrDefault(m => m.SessionDeCursusId == mes.SessionDeCursusId));
                }
                apprenant.MotDePasse = ap1.MotDePasse;
                context.SaveChanges();


            }

        }

        // DELETE: api/ApprenantAPI/5
        public void Delete(int id)
        {
          
            using (BDDContext context = new BDDContext())
            {

                Apprenant app = context.Apprenants.FirstOrDefault(m => m.ApprenantId == id);
                if(app.Messages !=null && app.Messages.Count != 0) {
                    context.Messages.Remove(context.Messages.FirstOrDefault(m => m.Apprenant.ApprenantId == id));
                    context.SaveChanges();
                }
                
                context.Apprenants.Remove(context.Apprenants.FirstOrDefault(k => k.ApprenantId == id));
                context.SaveChanges();

            }
           
        }
     

       
    }
}

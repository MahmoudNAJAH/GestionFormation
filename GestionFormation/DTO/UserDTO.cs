using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestionFormation.DTO
{
    /// <summary>
    /// La classe utiliser dans Session["userConnected"]
    /// Elle regroupe les informations de base de l'utilisateur pour être utilisée dans toute l'application
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public byte[] MotDePasse { get; set; }

        //Pour différencier Apprenant - Formateur - Admin
        public UserRole Role { get; set; }

        //On ne doit pas avoir d'utilisateur sans Role
        //Ou ca ferait rien ne fonctionnerait dans l'appli
        public UserDTO(UserRole Role)
        {
            this.Role = Role;
        }

        //Récupère le user correspondant à l'email donné
        public void GetUserFromEmail(string email)
        {
            //Le user a nécessairemetn un Role 
            //On switch dessus pour déterminer le DAO à utiliser
            //TODO: prévoir un retour défault ?
            switch (Role)
            {
                case UserRole.ATTENDANT:
                    Apprenant ap = ApprenantDAO.FindByLgMD(email);
                    if (ap != null)
                    {
                        Id = ap.ApprenantId;
                        Nom = ap.Nom;
                        Prenom = ap.Prenom;
                        Email = ap.Email;
                        MotDePasse = ap.MotDePasse;
                    }
                    break;

                case UserRole.FORMATEUR:
                    Formateur form = FormateurDAO.FindByLgMD(email);
                    if (form != null)
                    {
                        Id = form.FormateurId;
                        Nom = form.Nom;
                        Prenom = form.Prenom;
                        Email = form.Email;
                        MotDePasse = form.MotDePasse;
                    }
                    break;

                case UserRole.ADMIN:
                    Admin admin = AdminDAO.FindByLgMD(email);
                    if (admin != null)
                    {
                        Id = admin.AdminId;
                        Nom = admin.Nom;
                        Prenom = admin.Prenom;
                        Email = admin.Email;
                        MotDePasse = admin.MotDePasse;
                    }
                    break;
            }
        }

        //On récupère les SessionDeFormation de l'utilisateur
        public List<SessionDeFormation> GetSessionDeFormations()
        {
            List<SessionDeFormation> results = new List<SessionDeFormation>();

            switch(Role)
            {
                case UserRole.ATTENDANT:
                    List<SessionDeCursus> listSessionDeCursus = (ApprenantDAO.FindById(this.Id)).SessionDeCursus;
                    foreach (SessionDeCursus SessionCursus in listSessionDeCursus)
                    {
                        //on récupère les sessions Formations, mais leurs objets sont vides (Formation, Formateur etc..)
                        List<SessionDeFormation> listSessionFormations = new List<SessionDeFormation>();
                        listSessionFormations.AddRange(SessionDeCursusDAO.FindById(SessionCursus.SessionDeCursusId).SessionsDeFormations);

                        //On les recharge en récupérant leurs objets à l'aide de FindById
                        foreach (SessionDeFormation ses in listSessionFormations)
                            results.Add(SessionDeFormationDAO.FindById(ses.SessionDeFormationId));
                    }
                    break;

                case UserRole.FORMATEUR:
                    foreach (SessionDeFormation ses in FormateurDAO.FindById(this.Id).SessionDeFormations)
                        results.Add(SessionDeFormationDAO.FindById(ses.SessionDeFormationId));
                    break;

                case UserRole.ADMIN:
                    break;
            }

            return results;
        }

        //ATTENTION : changer la database => dans SessionDeFormation : List<SessionCursus>

        //On récupère les SessionDeCursus de l'utilisateur
        public List<SessionDeCursus> GetSessionDeCursus()
        {
            List<SessionDeCursus> results = new List<SessionDeCursus>();

            switch (Role)
            {
                case UserRole.ATTENDANT:
                    foreach(SessionDeCursus ses in ApprenantDAO.FindById(this.Id).SessionDeCursus)
                        results.Add(SessionDeCursusDAO.FindById(ses.SessionDeCursusId));
                    break;

                case UserRole.FORMATEUR:
                    //ATTENTION => changer la database
                    
                    List<SessionDeFormation> listSessionDeFormation = (FormateurDAO.FindById(this.Id)).SessionDeFormations;
                    foreach (SessionDeFormation SessionFormation in listSessionDeFormation)
                    {
                        //ATTENTION => changer la db pour avoir List<SessionDeCursus> dans SessionDeFormation
                        /*
                        //on récupère les sessions Formations, mais leurs objets sont vides (Formation, Formateur etc..)
                        List<SessionDeCursus> listSessionDeCursus = new List<SessionDeCursus>();
                        listSessionDeCursus.AddRange(SessionDeFormationDAO.FindById(SessionFormation.SessionDeFormationId).SessionDeCursus);
                        //On les recharge en récupérant leurs objets à l'aide de FindById
                        foreach (SessionDeCursus ses in listSessionDeCursus)
                            results.Add(SessionDeCursusDAO.FindById(ses.SessionDeCursusId));
                        */
                        results.Add(SessionDeCursusDAO.FindById(SessionFormation.SessionDeCursus.SessionDeCursusId));
                    }
                    break;

                case UserRole.ADMIN:
                    break;
            }

            return results;
        }

        //On récupère les Formation de l'utilisateur
        public List<Formation> GetFormations()
        {
            List<Formation> results = new List<Formation>();

            List<SessionDeFormation> listSessionDeFormation = GetSessionDeFormations();
            foreach (SessionDeFormation SessionDeFormation in listSessionDeFormation)
                results.Add(FormationDAO.FindById(SessionDeFormation.Formation.FormationId));

            return results;
        }

        //On récupère les Cursus de l'utilisateur
        public List<Cursus> GetCursus()
        {
            List<Cursus> results = new List<Cursus>();

            List<SessionDeCursus> listSessionDeCursus = GetSessionDeCursus();
            foreach(SessionDeCursus ses in listSessionDeCursus)
                results.Add(CursusDAO.FindById(ses.Cursus.CursusId));

            return results;
        }

        //TODO
        public List<Chat> GetChats()
        {
            List<Chat> results = new List<Chat>();

            // => Manque ChatDAO
            /*
            List<SessionDeCursus> listSessionDeCursus = GetSessionDeCursus();
            foreach (SessionDeCursus ses in listSessionDeCursus)
                results.Add(ChatDAO.FindById(ses.Chat.ChatId));
            */
            return results;
        }

        //TODO
        public List<Message> GetMessages()
        {
            List<Message> results = new List<Message>();


            

            return results;
        }

    }
}
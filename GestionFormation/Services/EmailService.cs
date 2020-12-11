using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace GestionFormation.Services
{
    /// <summary>
    /// Methode BOUCHONNEE !!!!!!!!
    /// On n'utilise qu'une seule adresse mail !!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    public class EmailService
    {

        public static void EnvoyerEmail(EmailAEnvoyerDTO emailAEnvoyer)
        {
            Apprenant ap = ApprenantDAO.FindById(emailAEnvoyer.FromId);
            string toEmail;

            //utile uniquement avec le bouchon
            string destinataire;

            if (emailAEnvoyer.ToId.StartsWith("A"))
            {
                // toEmail = AdminDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email;
                toEmail = Properties.Settings.Default.AdresseMail; // BOUCHON : envoi uniquement sur la boite du projet
                 destinataire = AdminDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email;
                Console.WriteLine($"Email envoyé (mais pas vraiment) à " + AdminDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email);

            }
            else
            {

                // toEmail = FormateurDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email;
                toEmail = Properties.Settings.Default.AdresseMail; // BOUCHON : envoi uniquement sur la boite du projet
                destinataire = FormateurDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email;


                Console.WriteLine($"Email envoyé (mais pas vraiment) à " + FormateurDAO.FindById(int.Parse(emailAEnvoyer.ToId.Substring(1))).Email);
            }


            //string fromEmail = ApprenantDAO.FindById(emailAEnvoyer.FromId).Email;
            string fromEmail = Properties.Settings.Default.AdresseMail; // BOUCHON : envoi uniquement sur la boite du projet

            using (MailMessage message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = emailAEnvoyer.Subject;
                message.Body = $"Message envoyé via l'application GestionFormationDawan par: {ap.Prenom} {ap.Nom}  \n\n {emailAEnvoyer.Content} \n\n MESSAGE AUTOMATIQUE. REPONDRE : {ap.Email}."
                    // A commenter si phase de prod
                    + $" \n\n envoyé à {destinataire}";


                if (emailAEnvoyer.AttachementPath != null)
                {
                    Attachment attachment = new Attachment(emailAEnvoyer.AttachementPath);
                    message.Attachments.Add(attachment);
                }


                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = Properties.Settings.Default.GmailHost;
                    client.Port = Properties.Settings.Default.GmailPort;

                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Properties.Settings.Default.AdresseMail, Properties.Settings.Default.MotDePasse);
                    client.EnableSsl = true;
                    client.Send(message);
                }

            }
        }

        public static List<UserDTO> GetDestinatairesPossibles(int id)
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (Admin a in AdminDAO.FindAll())
            {
                UserDTO u = new UserDTO
                {
                    Id = a.AdminId,
                    Nom = a.Nom,
                    Prenom = a.Prenom,
                    Role = UserRole.ADMIN
                    
                };
                users.Add(u);
            }
            List<Formateur> formateurs = FormateurDAO.FindByApprenantId(id);
            foreach (Formateur f in formateurs)
            {
                UserDTO u = new UserDTO
                {
                    Id = f.FormateurId,
                    Nom = f.Nom,
                    Prenom = f.Prenom,
                    Role = UserRole.FORMATEUR

                };
                users.Add(u);

            }

            return users;


        }

        public static string StockageTransitoire(string chemin, HttpPostedFileBase fichierAEnvoyer)
        {
            //sauvegarde sur le serveur
            fichierAEnvoyer.SaveAs(chemin);

            return chemin;
        }

        public static string Valider(HttpPostedFileBase fichierAEnvoyer)
        {
            if (fichierAEnvoyer == null || fichierAEnvoyer.ContentLength == 0)
            {
                return Properties.Resource.FichierVide;
            }
            else if (fichierAEnvoyer.ContentLength > Properties.Settings.Default.TailleMaxPieceJointe)
            {
                return Properties.Resource.FichierTropVolumineux;
            }
            else
                return Properties.Resource.FichierEnvoye;



        }
    }
}
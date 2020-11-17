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
    public class EmailService
    {

        public static void EnvoyerEmail(EmailAEnvoyerDTO emailAEnvoyer)
        {

            //string toEmail = AdminDAO.FindById(emailAEnvoyer.ToId).Email;
            string toEmail = Properties.Resource.AdresseMail; // BOUCHON : envoi uniquement sur la boite du projet

            //string fromEmail = ApprenantDAO.FindById(emailAEnvoyer.FromId).Email;
            string fromEmail = Properties.Resource.AdresseMail; // BOUCHON : envoi uniquement sur la boite du projet

            using (MailMessage message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = emailAEnvoyer.Subject;
                message.Body = emailAEnvoyer.Content;


                if (emailAEnvoyer.AttachementPath != null)
                {
                    Attachment attachment = new Attachment(emailAEnvoyer.AttachementPath);
                    message.Attachments.Add(attachment);

                }


                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = Properties.Resource.GmailHost;
                    client.Port = int.Parse(Properties.Resource.GmailPort);

                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Properties.Resource.AdresseMail, Properties.Resource.MotDePasse);
                    client.EnableSsl = true;
                    client.Send(message);
                }

            }
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
            else if (fichierAEnvoyer.ContentLength > int.Parse(Properties.Resource.TailleMaxPieceJointe))
            {
                return Properties.Resource.FichierTropVolumineux;
            }
            else
                return Properties.Resource.FichierEnvoye;



        }
    }
}
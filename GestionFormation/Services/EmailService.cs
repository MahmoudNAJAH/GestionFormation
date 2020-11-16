using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
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

            string fromEmail = ApprenantDAO.FindById(emailAEnvoyer.FromId).Email;

            using (MailMessage message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = emailAEnvoyer.Subject;
                message.Body = emailAEnvoyer.Content;

                
                Attachment attachment = new Attachment();
                message.Attachments.Add(attachment);


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
    }
}
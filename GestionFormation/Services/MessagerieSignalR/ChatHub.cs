using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using Microsoft.AspNet.SignalR;

namespace GestionFormation.Services.MessagerieSignalR
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, UserForChatDTO> Dico = new Dictionary<string, UserForChatDTO>();

        public void Send(string name, string message, string salon, string senderId)
        //public void Send(string name, string message, string salon)
        {
            string date= $"Aujourd'hui {DateTime.Now.ToString("H:mm")}";
            Message m = new Message
            {
                Contenu = message,
                DateDePublication = DateTime.Now,
            };

            //ChatDAO.AddMessageToDB(m, int.Parse(salon) , int.Parse(senderId));
            ChatDAO.AddMessageToDB(m, int.Parse(salon) , int.Parse(senderId));

            // Call the addNewMessageToPage method to update clients.
            Clients.Group(salon).addNewMessageToPage(name, message, date);
        }

       
   
        public void JoinRoom(string roomName)
        {
             Groups.Add(Context.ConnectionId, roomName);

        }

        public void LeaveRoom(string roomName)
        {
             Groups.Remove(Context.ConnectionId, roomName);
        }

        /// <summary>
        /// Recupératon des 100  derniers messages du groupe en BDD
        /// </summary>
        /// <param name="salon"></param>
        public void RecuperatioDesMessagesEnBDD(string salon)
        {
            Chat chat = ChatDAO.FindBySessionDeCursusId(int.Parse(salon));

            List<Message> messages = chat.Messages.OrderBy(m => m.DateDePublication).Take(Properties.Settings.Default.NbMessageChatRecuperesEnBDDALaConnexion).ToList();
            foreach (Message m in messages)
            {
                string sender = $"{m.Apprenant.Prenom}" + " " + $"{m.Apprenant.Nom.Substring(0, 1)}.";
                string message = m.Contenu;
                string date = (m.DateDePublication.DayOfYear == DateTime.Now.DayOfYear && m.DateDePublication.Year == DateTime.Now.Year) ?
                     $"Aujourd'hui {m.DateDePublication.ToString("H:mm")}" :  m.DateDePublication.ToString("dd MMM H:mm");
                Clients.Caller.addNewMessageToPage(sender, message, date);

            }
        }



    }
}
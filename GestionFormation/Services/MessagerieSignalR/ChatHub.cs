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
        /// <summary>
        /// 1- salon
        /// 2- connectionId
        /// 3- nom
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> ListAllUsersConnected = new Dictionary<string, Dictionary<string, string>>();

        public void Send(string name, string message, string roomName, string senderId)
        //public void Send(string name, string message, string salon)
        {
            string date= $"Aujourd'hui {DateTime.Now.ToString("H:mm")}";
            Message m = new Message
            {
                Contenu = message,
                DateDePublication = DateTime.Now,
            };

            //ChatDAO.AddMessageToDB(m, int.Parse(salon) , int.Parse(senderId));
            ChatDAO.AddMessageToDB(m, int.Parse(roomName) , int.Parse(senderId));

            // Call the addNewMessageToPage method to update clients.
            Clients.Group(roomName).addNewMessageToPage(name, message, date);
        }

       
   /// <summary>
   ///  Rejoint la salle
   ///  Envoi à tous la liste des utilisateurs connectés
   /// </summary>
   /// <param name="roomName"></param>
   /// <param name="name"></param>
        public void JoinRoom(string roomName, string name)
        {
            Groups.Add(Context.ConnectionId, roomName);
            Dictionary<string, string> UserConnectedInthisRoom;
            if (!ListAllUsersConnected.ContainsKey(roomName))
            {
                 UserConnectedInthisRoom = new Dictionary<string, string>();
                ListAllUsersConnected.Add(roomName, UserConnectedInthisRoom);
            }
            else
            {
                 UserConnectedInthisRoom = ListAllUsersConnected[roomName];
            }

            UserConnectedInthisRoom.Add(Context.ConnectionId, name);

            SendListUserConnected(UserConnectedInthisRoom, roomName);


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

        /// <summary>
        /// On retire la personne à la déconnexion
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {

            foreach (var item in ListAllUsersConnected)
            {
                if (item.Value.ContainsKey(Context.ConnectionId))
                {
                    Dictionary<string, string>  UserConnectedInthisRoom = new Dictionary<string, string>();
                    string roomName = item.Key;
                    UserConnectedInthisRoom = item.Value;
                    UserConnectedInthisRoom.Remove(Context.ConnectionId);

                    //Renvoi de la list
                    SendListUserConnected(UserConnectedInthisRoom, roomName);
                    Groups.Remove(Context.ConnectionId, roomName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Appelé à la connexion ou a la déconnexion d'un utilisateur
        /// </summary>
        /// <param name="UserConnectedInthisRoom"></param>
        /// <param name="roomName"></param>
        private void SendListUserConnected(Dictionary<string, string> UserConnectedInthisRoom, string roomName)
        {
            string listName = "";
            foreach (string user in UserConnectedInthisRoom.Values)
            {
                listName += user;
                listName += " - ";
            }
            listName = listName.Substring(0, listName.Length - 3);

            Clients.Group(roomName).sendListName(listName);
        }
    }
}
using System;
using System.Collections.Generic;
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

        public void Send(string name, string message, string salon)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Group(salon).addNewMessageToPage(name, message);
        }

        //public void Send(string senderId, string message)
        //{

        //    Apprenant ap = ApprenantDAO.FindById(int.Parse(senderId));
          
        //    Clients.Group("bob").addChatMessage(senderId, message);


        //}
   
        public void JoinRoom(string roomName)
        {
             Groups.Add(Context.ConnectionId, roomName);
        }

        public void LeaveRoom(string roomName)
        {
             Groups.Remove(Context.ConnectionId, roomName);
        }

        public override Task OnConnected()
        {
            var context = Context;
            string room = ("");
            JoinRoom(room);

            return base.OnConnected();
    }



}
}
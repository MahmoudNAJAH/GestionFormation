using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GestionFormation.DAO;
using GestionFormation.Entities;
using Microsoft.AspNet.SignalR;

namespace GestionFormation.Services.MessagerieSignalR
{
    public class ChatHub : Hub
    {
        public void Send(string senderId, string message)
        {
            // Clients.All.addNewMessageToPage(name, message);

            Apprenant ap = ApprenantDAO.FindById(int.Parse(senderId));
          
            Clients.Group("groupName").addChatMessage(senderId, message);


        }

        public Task JoinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            var context = Context;

                    // Add to each assigned group.
                    //foreach (var item in user.Rooms)
                    //{
                    //    Groups.Add(Context.ConnectionId, item.RoomName);
                    //}

                    
                
            
            return base.OnConnected();
        }
    }
}
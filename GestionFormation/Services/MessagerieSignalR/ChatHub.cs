using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var item in ap.SessionDeCursus)
            {

            }
                Clients.Group(ap.SessionDeCursus).


        }
    }
}
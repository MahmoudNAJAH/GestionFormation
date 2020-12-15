using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Services.MessagerieSignalR
{
    public class ListUserConnectedSingleton
    {
            private static ListUserConnectedSingleton instance;

            // Objet utilisé pour verrouiller le traitement
            private static object instanceLock = new object();


        /// <summary>
        /// 1e string - salon
        /// 2e string - connectionId
        /// 3e string - nom
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> ListAllUsersConnected;
        public bool HasChanged { get; set; }


        private ListUserConnectedSingleton()
            {
                if (ListAllUsersConnected==null)
                {
                // Création de la liste
                ListAllUsersConnected = new Dictionary<string, Dictionary<string, string>>();
            }
        }

            public static ListUserConnectedSingleton GetInstance()
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            // On retarde l'instanciation à la première utilisation
                            instance = new ListUserConnectedSingleton();
                        }
                    }
                }

                return instance;
            }

        public void AddInThisUserRoom(string roomName, string name, string ConnectionId)
        {
            lock (instanceLock)
            {
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
                UserConnectedInthisRoom.Add(ConnectionId, name);
            }
        }

        internal Dictionary<string, string> getUserConnectedInthisRoom(string roomName)
        {
            lock (instanceLock)
            {
                Dictionary<string, string> UserConnectedInthisRoom = ListAllUsersConnected[roomName];
                Dictionary<string, string> UserConnectedInthisRoomCopy = UserConnectedInthisRoom.ToDictionary(entry => entry.Key, entry => entry.Value);
                return UserConnectedInthisRoomCopy;

            }
        }

        public string RemoveThisUser(string ConnectionId)
        {
            Dictionary<string, string> UserConnectedInthisRoom = new Dictionary<string, string>();
            string roomName = string.Empty;
            foreach (var item in ListAllUsersConnected)
            {
                if (item.Value.ContainsKey(ConnectionId))
                {
                     roomName = item.Key;
                    UserConnectedInthisRoom = item.Value;
                    lock (instanceLock)
                    {
                        UserConnectedInthisRoom.Remove(ConnectionId);

                    }
                }
            }
            return roomName;
        }


       
    }
}
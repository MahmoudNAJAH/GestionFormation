using GestionFormation.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace GestionFormation.DAO
{
    public class ChatDAO
    {

        public static Chat FindBySessionDeCursusId(int sdcId)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                Chat chat = context.Chats.Include(c=> c.Messages).FirstOrDefault(c => c.SessionDeCursus.SessionDeCursusId == sdcId);
                List<Message> lm = new List<Message>();
                foreach (Message m in chat.Messages)
                {
                    lm.Add(context.Messages.Include(mess => mess.Apprenant).FirstOrDefault(mess => mess.MessageId == m.MessageId));
                }
                chat.Messages = lm;
                return chat;
            }
        }

        public static void AddMessageToDB(Message m, int sdcId, int senderId)
        {
            using (BDDContext context = new BDDContext())
            {
                Apprenant a = context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == senderId);
                Chat chat = context.Chats.FirstOrDefault(c => c.SessionDeCursus.SessionDeCursusId == sdcId);
                if (chat == null)
                {
                    chat = new Chat
                    {
                        SessionDeCursus = context.SessionDeCursus.FirstOrDefault(sdc => sdc.SessionDeCursusId == sdcId),
                    };
                    context.Chats.Add(chat);

                }
                if (chat.Messages == null) chat.Messages = new List<Message>();

                Message message = new Message
                {
                    Contenu = m.Contenu,
                    Apprenant = a,
                    DateDePublication = m.DateDePublication
                };

                chat.Messages.Add(message);
                context.SaveChanges();
            }
}
    }
}
using System.Collections.Generic;

namespace GestionFormation.Entities
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int SessionDeCursusId { get; set; }
        public SessionDeCursus SessionDeCursus { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime DateDePublication { get; set; }
        public Apprenant Apprenant { get; set; }
    }
}
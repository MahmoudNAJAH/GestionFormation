using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace GestionFormation.Entities
{
    public class Apprenant
    {
        public int ApprenantId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
 
        public byte[]  MotDePasse { get; set; }
        public virtual List<SessionDeCursus> SessionDeCursus { get; set; }
        public virtual List<Message> Messages { get; set; }

    }
}
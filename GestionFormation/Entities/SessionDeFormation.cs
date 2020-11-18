using System;
using System.Collections.Generic;

namespace GestionFormation.Entities
{
    public class SessionDeFormation
    {
        public int SessionDeFormationId { get; set; }
        public DateTime DateDebut {get;set;}
        public Formateur Formateur { get; set; }
        public Formation Formation { get; set; }
        public SessionDeCursus SessionDeCursus { get; set; }

    }
}
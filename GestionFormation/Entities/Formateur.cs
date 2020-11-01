using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Entities
{
    public class Formateur
    {

        public int FormateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
       public virtual List<SessionDeFormation> SessionDeFormations { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
  public  class Formateur
    {
        public int FormateurId { get; set; }
        public string Nom { get; set;  }
        public string Prenom { get; set; }
        public string Mot_De_Passe { get; set; }
        [MaxLength(50)]
        public string E_mail { get; set; }

        List<Session_De_Formation> SessionsFormations { get; set; }
    }
}

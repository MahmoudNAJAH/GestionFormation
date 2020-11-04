using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
   public class Stagiaire
    {
        public int StagiaireId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [MaxLength(50)]
        public string E_mail { get; set;  }
        public string Url { get; set; }
        public DateTime Date_de_Naissance { get; set; }
        [MaxLength(8)]
        public string Mot_De_Passe { get; set; }
        List<Session_De_Cursus> SessionsCursus { get; set; }
    }
}

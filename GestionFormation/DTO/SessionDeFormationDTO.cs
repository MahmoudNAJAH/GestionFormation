using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class SessionDeFormationDTO
    {
        public int SessionDeFormationId { get; set; }
        public int FormateurId { get; set; }
        [DisplayName("Nom du Formateur")]
        public string NomFormateur { get; set; }
        public int FormationId { get; set; }
        [DisplayName("Nom de la Formation")]
        public string NomFormation { get; set; }
        public int SessionDeCursusId { get; set; }
    }
}
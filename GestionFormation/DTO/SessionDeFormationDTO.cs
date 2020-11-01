using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class SessionDeFormationDTO
    {
        public int SessionDeFormationId { get; set; }
        public int FormateurId { get; set; }
        public string NomFormateur { get; set; }
        public int FormationId { get; set; }
        public string NomFormation { get; set; }
        public int SessionDeCursusId { get; set; }
    }
}
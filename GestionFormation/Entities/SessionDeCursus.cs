using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFormation.Entities
{
    public class SessionDeCursus
    {
        public int SessionDeCursusId { get; set; }
        public virtual List<Apprenant> Apprenants { get; set; }
        public virtual List<SessionDeFormation> SessionsDeFormations { get; set; }
        public Cursus Cursus { get; set; }
    }
}
using System.Collections.Generic;

namespace GestionFormation.Entities
{
    public class Formation
    {
        public int FormationId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Dure { get; set; }
        public virtual List<Cursus> Cursus { get; set; }
        public virtual List<SessionDeFormation> SessionsDeFormations { get; set; }
    }
}
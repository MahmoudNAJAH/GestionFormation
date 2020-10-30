using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class Formation
    {
        public int FormationId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public virtual List<Cursus> Cursus { get; set; }
        public virtual List<FormationSession> FormationSessions { get; set; }
    }
}

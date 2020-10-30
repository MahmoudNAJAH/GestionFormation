using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class FormationSession
    {
        public int FormationSessionId { get; set; }
        public int FormateurId { get; set; }
        public int FormationId { get; set; }
        public virtual List<CursusSession> Sessions { get; set; }
    }
}

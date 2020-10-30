using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class CursusSession
    {
        public int CursusSessionId { get; set; }
        public int CursusId { get; set; }
        public virtual List<Attendant> Attendants { get; set; }
        public virtual List<FormationSession> FormationSessions { get; set; }
    }
}

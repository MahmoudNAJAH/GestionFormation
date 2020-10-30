using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class Attendant : Personne
    {
        public int AttendantId { get; set; }
        public virtual List<CursusSession> CursusSessions { get; set; }
    }
}

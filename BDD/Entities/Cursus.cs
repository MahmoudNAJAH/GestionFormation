using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class Cursus
    {
        public int CursusId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public virtual List<CursusSession> CursusSessions { get; set; }
        public virtual List<Formation> Formations { get; set; }
    }
}

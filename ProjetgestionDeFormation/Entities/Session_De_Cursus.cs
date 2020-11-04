using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
 public   class Session_De_Cursus
    {
        public int Session_De_CursusId { get; set; }
        public string Description { get; set; }
        public string url { get; set; }
        public List<Stagiaire> Stagiaires { get; set; }

        public List<Session_De_Formation> SessionFormations { get; set; }
    }
}

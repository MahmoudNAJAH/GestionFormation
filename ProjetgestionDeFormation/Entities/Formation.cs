using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
  public  class Formation
    {
        public int FormationId { get; set; }
        public string titre { get; set; }
        public string Description { get; set; }
        public List<Cursus> LesCursus { get; set; }
        public List<Session_De_Formation>SessionDeFormations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
  public  class Session_De_Formation
    {
        public int Session_De_FormationId { get; set; }
        public string Description { get; set; }
        public List<Formateur> Formateurs { get; set; }
        public List<Session_De_Cursus> SessionCursus { get; set; }

    }
}

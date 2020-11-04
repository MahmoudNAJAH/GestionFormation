using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
  public  class Cursus
    {
        public int CursusId { get; set; }
        public string titre { get; set; }
        public string Description { get; set; }
       public List<Formation> Formations { get; set; }
        public List<Session_De_Cursus> CursusSuivi { get; set; }
    }
}

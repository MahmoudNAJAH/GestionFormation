using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Entities
{
    public class Cursus
    {
        public int CursusId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public  List<Formation> Formations { get; set; }
        public  List<SessionDeCursus> SessionDeCursus { get; set; }
    }
}
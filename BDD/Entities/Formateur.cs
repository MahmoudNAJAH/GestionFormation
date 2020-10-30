using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class Formateur : Personne
    {
        public int FormateurId { get; set; }
        public virtual List<FormationSession> FormationSessions { get; set; }
    }
}

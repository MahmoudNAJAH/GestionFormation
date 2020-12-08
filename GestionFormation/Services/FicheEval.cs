using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class FicheEval
    {
        public int niveauAcceuil { get; set; }
        public string Excellent { get; set; }
    }
    public class ListFicheEval
    {
       List<FicheEval> MyList { get; set; }
    }
}
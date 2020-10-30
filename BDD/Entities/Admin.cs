using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class Admin 
    {
        public int AdminId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Age { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string AdresseMail { get; set; }
    }
}

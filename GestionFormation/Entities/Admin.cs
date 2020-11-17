using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Entities
{
    public class Admin
    {

        public int AdminId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public byte[] MotDePasse { get; set; }
    }
}
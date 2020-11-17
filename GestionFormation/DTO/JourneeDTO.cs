using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class JourneeDTO
    {
        public DateTime Date { get; set; }
        public Formation Formation { get; set; }
        public Formateur Formateur { get; set; }
    }
}
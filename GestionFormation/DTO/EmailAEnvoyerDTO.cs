using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class EmailAEnvoyerDTO
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
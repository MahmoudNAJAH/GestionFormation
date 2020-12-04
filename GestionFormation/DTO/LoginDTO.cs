using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class LoginDTO
    {

        [Required]
        [DisplayName("Identifiant")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Mot de passe")]
        
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
    }
}
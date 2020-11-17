using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

    }
}
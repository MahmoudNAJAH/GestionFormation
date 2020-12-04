using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class BDDContext : DbContext
    {
        public virtual DbSet<Apprenant> Apprenants { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Cursus> Cursus { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Formateur> Formateurs { get; set; }
        public virtual DbSet<Formation> Formations { get; set; }
        public virtual DbSet<SessionDeCursus> SessionDeCursus { get; set; }
        public virtual DbSet<SessionDeFormation> SessionDeFormations { get; set; }

        public System.Data.Entity.DbSet<GestionFormation.DTO.UserDTO> UserDTOes { get; set; }
    }
}
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
        public DbSet<Apprenant> Apprenants { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Cursus> Cursus { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<SessionDeCursus> SessionDeCursus { get; set; }
        public DbSet<SessionDeFormation> SessionDeFormations { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetdawan.Entities
{
    class BDDContext:DbContext
    {
        public DbSet<Cursus> Cursuss { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Stagiaire> Stagiaires { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Session_De_Formation> SessionFormations { get; set; }
        public DbSet<Session_De_Cursus> SessionCursus { get; set; }

    }
}

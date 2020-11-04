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
        public DbSet<Cursus> Cursuss;
        public DbSet<Formation> Formations;
        public DbSet<Formateur> Formateurs;
        public DbSet<Stagiaire> Stagiaires;
        public DbSet<Message> Messages;
        public DbSet<Chat> Chats;
        public DbSet<Session_De_Formation> SessionFormations;
        public DbSet<Session_De_Cursus> SessionCursus;

    }
}

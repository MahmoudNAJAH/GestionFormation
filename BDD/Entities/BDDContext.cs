using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD.Entities
{
    public class BDDContext : DbContext
    {
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Cursus> Cursus { get; set; }
        public DbSet<CursusSession> CursusSessions { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<FormationSession> FormationSessions { get; set; }
    }
}

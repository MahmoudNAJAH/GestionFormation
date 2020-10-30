using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BDD
{
    public class BDDContext : DbContext
    {
        public DbSet<Cursus> Cursus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DAO
{
    public class Class1
    {
        public void Populate()
        {
            using (BDDContext context = new BDDContext())
            {
                context.Database.ExecuteSqlCommand(
        "UPDATE dbo.Blogs SET Name = 'Another Name' WHERE BlogId = 1"
        
        );
            }
        }
    }
}
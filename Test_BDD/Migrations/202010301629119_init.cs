namespace Test_BDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursus",
                c => new
                    {
                        CursusId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CursusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cursus");
        }
    }
}

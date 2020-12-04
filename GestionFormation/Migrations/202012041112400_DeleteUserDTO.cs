namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserDTO : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserDTOes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        MotDePasse = c.Binary(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}

namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddb5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Apprenants", "Identifiants_UserLoginId", "dbo.UserLogins");
            DropIndex("dbo.Apprenants", new[] { "Identifiants_UserLoginId" });
            AddColumn("dbo.Apprenants", "Email", c => c.String());
            AddColumn("dbo.Apprenants", "MotDePasse", c => c.String());
            DropColumn("dbo.Apprenants", "Identifiants_UserLoginId");
            DropTable("dbo.UserLogins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserLoginId = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserLoginId);
            
            AddColumn("dbo.Apprenants", "Identifiants_UserLoginId", c => c.Int());
            DropColumn("dbo.Apprenants", "MotDePasse");
            DropColumn("dbo.Apprenants", "Email");
            CreateIndex("dbo.Apprenants", "Identifiants_UserLoginId");
            AddForeignKey("dbo.Apprenants", "Identifiants_UserLoginId", "dbo.UserLogins", "UserLoginId");
        }
    }
}

namespace BDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class heritageAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Nom", c => c.String());
            AddColumn("dbo.Admins", "Prenom", c => c.String());
            AddColumn("dbo.Admins", "Age", c => c.String());
            AddColumn("dbo.Admins", "Login", c => c.String());
            AddColumn("dbo.Admins", "Mdp", c => c.String());
            AddColumn("dbo.Admins", "AdresseMail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AdresseMail");
            DropColumn("dbo.Admins", "Mdp");
            DropColumn("dbo.Admins", "Login");
            DropColumn("dbo.Admins", "Age");
            DropColumn("dbo.Admins", "Prenom");
            DropColumn("dbo.Admins", "Nom");
        }
    }
}

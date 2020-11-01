namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutAdminmotdepasse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apprenants", "MotDePasse", c => c.String());
            AddColumn("dbo.Formateurs", "MotDePasse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Formateurs", "MotDePasse");
            DropColumn("dbo.Apprenants", "MotDePasse");
        }
    }
}

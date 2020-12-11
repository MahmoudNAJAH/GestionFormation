namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutdecontenudansMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Contenu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Contenu");
        }
    }
}

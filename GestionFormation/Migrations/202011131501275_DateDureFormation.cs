namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateDureFormation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formations", "Dure", c => c.Int(nullable: false));
            AddColumn("dbo.SessionDeFormations", "DateDebut", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionDeFormations", "DateDebut");
            DropColumn("dbo.Formations", "Dure");
        }
    }
}

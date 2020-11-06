namespace ProjetgestionDeFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajouturl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ChatId);
            
            CreateTable(
                "dbo.Cursus",
                c => new
                    {
                        CursusId = c.Int(nullable: false, identity: true),
                        titre = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.CursusId);
            
            CreateTable(
                "dbo.Session_De_Cursus",
                c => new
                    {
                        Session_De_CursusId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        url = c.String(),
                        Cursus_CursusId = c.Int(),
                    })
                .PrimaryKey(t => t.Session_De_CursusId)
                .ForeignKey("dbo.Cursus", t => t.Cursus_CursusId)
                .Index(t => t.Cursus_CursusId);
            
            CreateTable(
                "dbo.Session_De_Formation",
                c => new
                    {
                        Session_De_FormationId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Formation_FormationId = c.Int(),
                    })
                .PrimaryKey(t => t.Session_De_FormationId)
                .ForeignKey("dbo.Formations", t => t.Formation_FormationId)
                .Index(t => t.Formation_FormationId);
            
            CreateTable(
                "dbo.Formateurs",
                c => new
                    {
                        FormateurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Mot_De_Passe = c.String(),
                        E_mail = c.String(maxLength: 50),
                        Session_De_Formation_Session_De_FormationId = c.Int(),
                    })
                .PrimaryKey(t => t.FormateurId)
                .ForeignKey("dbo.Session_De_Formation", t => t.Session_De_Formation_Session_De_FormationId)
                .Index(t => t.Session_De_Formation_Session_De_FormationId);
            
            CreateTable(
                "dbo.Stagiaires",
                c => new
                    {
                        StagiaireId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        E_mail = c.String(maxLength: 50),
                        Url = c.String(),
                        Date_de_Naissance = c.DateTime(nullable: false),
                        Mot_De_Passe = c.String(maxLength: 8),
                        Session_De_Cursus_Session_De_CursusId = c.Int(),
                    })
                .PrimaryKey(t => t.StagiaireId)
                .ForeignKey("dbo.Session_De_Cursus", t => t.Session_De_Cursus_Session_De_CursusId)
                .Index(t => t.Session_De_Cursus_Session_De_CursusId);
            
            CreateTable(
                "dbo.Formations",
                c => new
                    {
                        FormationId = c.Int(nullable: false, identity: true),
                        titre = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FormationId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "dbo.Session_De_FormationSession_De_Cursus",
                c => new
                    {
                        Session_De_Formation_Session_De_FormationId = c.Int(nullable: false),
                        Session_De_Cursus_Session_De_CursusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Session_De_Formation_Session_De_FormationId, t.Session_De_Cursus_Session_De_CursusId })
                .ForeignKey("dbo.Session_De_Formation", t => t.Session_De_Formation_Session_De_FormationId, cascadeDelete: true)
                .ForeignKey("dbo.Session_De_Cursus", t => t.Session_De_Cursus_Session_De_CursusId, cascadeDelete: true)
                .Index(t => t.Session_De_Formation_Session_De_FormationId)
                .Index(t => t.Session_De_Cursus_Session_De_CursusId);
            
            CreateTable(
                "dbo.FormationCursus",
                c => new
                    {
                        Formation_FormationId = c.Int(nullable: false),
                        Cursus_CursusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Formation_FormationId, t.Cursus_CursusId })
                .ForeignKey("dbo.Formations", t => t.Formation_FormationId, cascadeDelete: true)
                .ForeignKey("dbo.Cursus", t => t.Cursus_CursusId, cascadeDelete: true)
                .Index(t => t.Formation_FormationId)
                .Index(t => t.Cursus_CursusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Session_De_Formation", "Formation_FormationId", "dbo.Formations");
            DropForeignKey("dbo.FormationCursus", "Cursus_CursusId", "dbo.Cursus");
            DropForeignKey("dbo.FormationCursus", "Formation_FormationId", "dbo.Formations");
            DropForeignKey("dbo.Session_De_Cursus", "Cursus_CursusId", "dbo.Cursus");
            DropForeignKey("dbo.Stagiaires", "Session_De_Cursus_Session_De_CursusId", "dbo.Session_De_Cursus");
            DropForeignKey("dbo.Session_De_FormationSession_De_Cursus", "Session_De_Cursus_Session_De_CursusId", "dbo.Session_De_Cursus");
            DropForeignKey("dbo.Session_De_FormationSession_De_Cursus", "Session_De_Formation_Session_De_FormationId", "dbo.Session_De_Formation");
            DropForeignKey("dbo.Formateurs", "Session_De_Formation_Session_De_FormationId", "dbo.Session_De_Formation");
            DropIndex("dbo.FormationCursus", new[] { "Cursus_CursusId" });
            DropIndex("dbo.FormationCursus", new[] { "Formation_FormationId" });
            DropIndex("dbo.Session_De_FormationSession_De_Cursus", new[] { "Session_De_Cursus_Session_De_CursusId" });
            DropIndex("dbo.Session_De_FormationSession_De_Cursus", new[] { "Session_De_Formation_Session_De_FormationId" });
            DropIndex("dbo.Stagiaires", new[] { "Session_De_Cursus_Session_De_CursusId" });
            DropIndex("dbo.Formateurs", new[] { "Session_De_Formation_Session_De_FormationId" });
            DropIndex("dbo.Session_De_Formation", new[] { "Formation_FormationId" });
            DropIndex("dbo.Session_De_Cursus", new[] { "Cursus_CursusId" });
            DropTable("dbo.FormationCursus");
            DropTable("dbo.Session_De_FormationSession_De_Cursus");
            DropTable("dbo.Messages");
            DropTable("dbo.Formations");
            DropTable("dbo.Stagiaires");
            DropTable("dbo.Formateurs");
            DropTable("dbo.Session_De_Formation");
            DropTable("dbo.Session_De_Cursus");
            DropTable("dbo.Cursus");
            DropTable("dbo.Chats");
        }
    }
}

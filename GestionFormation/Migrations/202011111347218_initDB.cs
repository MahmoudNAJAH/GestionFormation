namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        MotDePasse = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Apprenants",
                c => new
                    {
                        ApprenantId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        MotDePasse = c.String(),
                    })
                .PrimaryKey(t => t.ApprenantId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        DateDePublication = c.DateTime(nullable: false),
                        ApprenantId = c.Int(nullable: false),
                        Chat_ChatId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Apprenants", t => t.ApprenantId, cascadeDelete: true)
                .ForeignKey("dbo.Chats", t => t.Chat_ChatId)
                .Index(t => t.ApprenantId)
                .Index(t => t.Chat_ChatId);
            
            CreateTable(
                "dbo.SessionDeCursus",
                c => new
                    {
                        SessionDeCursusId = c.Int(nullable: false, identity: true),
                        CursusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionDeCursusId)
                .ForeignKey("dbo.Cursus", t => t.CursusId, cascadeDelete: true)
                .Index(t => t.CursusId);
            
            CreateTable(
                "dbo.Cursus",
                c => new
                    {
                        CursusId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CursusId);
            
            CreateTable(
                "dbo.Formations",
                c => new
                    {
                        FormationId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FormationId);
            
            CreateTable(
                "dbo.SessionDeFormations",
                c => new
                    {
                        SessionDeFormationId = c.Int(nullable: false, identity: true),
                        FormateurId = c.Int(nullable: false),
                        FormationId = c.Int(nullable: false),
                        SessionDeCursusId = c.Int(nullable: false),
                        Formateur_FormateurId = c.Int(),
                        Formateur_FormateurId1 = c.Int(),
                        Formation_FormateurId = c.Int(),
                    })
                .PrimaryKey(t => t.SessionDeFormationId)
                .ForeignKey("dbo.Formateurs", t => t.Formateur_FormateurId)
                .ForeignKey("dbo.Formateurs", t => t.Formateur_FormateurId1)
                .ForeignKey("dbo.Formateurs", t => t.Formation_FormateurId)
                .ForeignKey("dbo.SessionDeCursus", t => t.SessionDeCursusId, cascadeDelete: true)
                .ForeignKey("dbo.Formations", t => t.FormationId, cascadeDelete: true)
                .Index(t => t.FormationId)
                .Index(t => t.SessionDeCursusId)
                .Index(t => t.Formateur_FormateurId)
                .Index(t => t.Formateur_FormateurId1)
                .Index(t => t.Formation_FormateurId);
            
            CreateTable(
                "dbo.Formateurs",
                c => new
                    {
                        FormateurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        MotDePasse = c.String(),
                    })
                .PrimaryKey(t => t.FormateurId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        SessionDeCursusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.SessionDeCursus", t => t.SessionDeCursusId, cascadeDelete: true)
                .Index(t => t.SessionDeCursusId);
            
            CreateTable(
                "dbo.SessionDeCursusApprenants",
                c => new
                    {
                        SessionDeCursus_SessionDeCursusId = c.Int(nullable: false),
                        Apprenant_ApprenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionDeCursus_SessionDeCursusId, t.Apprenant_ApprenantId })
                .ForeignKey("dbo.SessionDeCursus", t => t.SessionDeCursus_SessionDeCursusId, cascadeDelete: true)
                .ForeignKey("dbo.Apprenants", t => t.Apprenant_ApprenantId, cascadeDelete: true)
                .Index(t => t.SessionDeCursus_SessionDeCursusId)
                .Index(t => t.Apprenant_ApprenantId);
            
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
            DropForeignKey("dbo.Chats", "SessionDeCursusId", "dbo.SessionDeCursus");
            DropForeignKey("dbo.Messages", "Chat_ChatId", "dbo.Chats");
            DropForeignKey("dbo.SessionDeCursus", "CursusId", "dbo.Cursus");
            DropForeignKey("dbo.SessionDeFormations", "FormationId", "dbo.Formations");
            DropForeignKey("dbo.SessionDeFormations", "SessionDeCursusId", "dbo.SessionDeCursus");
            DropForeignKey("dbo.SessionDeFormations", "Formation_FormateurId", "dbo.Formateurs");
            DropForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId1", "dbo.Formateurs");
            DropForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId", "dbo.Formateurs");
            DropForeignKey("dbo.FormationCursus", "Cursus_CursusId", "dbo.Cursus");
            DropForeignKey("dbo.FormationCursus", "Formation_FormationId", "dbo.Formations");
            DropForeignKey("dbo.SessionDeCursusApprenants", "Apprenant_ApprenantId", "dbo.Apprenants");
            DropForeignKey("dbo.SessionDeCursusApprenants", "SessionDeCursus_SessionDeCursusId", "dbo.SessionDeCursus");
            DropForeignKey("dbo.Messages", "ApprenantId", "dbo.Apprenants");
            DropIndex("dbo.FormationCursus", new[] { "Cursus_CursusId" });
            DropIndex("dbo.FormationCursus", new[] { "Formation_FormationId" });
            DropIndex("dbo.SessionDeCursusApprenants", new[] { "Apprenant_ApprenantId" });
            DropIndex("dbo.SessionDeCursusApprenants", new[] { "SessionDeCursus_SessionDeCursusId" });
            DropIndex("dbo.Chats", new[] { "SessionDeCursusId" });
            DropIndex("dbo.SessionDeFormations", new[] { "Formation_FormateurId" });
            DropIndex("dbo.SessionDeFormations", new[] { "Formateur_FormateurId1" });
            DropIndex("dbo.SessionDeFormations", new[] { "Formateur_FormateurId" });
            DropIndex("dbo.SessionDeFormations", new[] { "SessionDeCursusId" });
            DropIndex("dbo.SessionDeFormations", new[] { "FormationId" });
            DropIndex("dbo.SessionDeCursus", new[] { "CursusId" });
            DropIndex("dbo.Messages", new[] { "Chat_ChatId" });
            DropIndex("dbo.Messages", new[] { "ApprenantId" });
            DropTable("dbo.FormationCursus");
            DropTable("dbo.SessionDeCursusApprenants");
            DropTable("dbo.Chats");
            DropTable("dbo.Formateurs");
            DropTable("dbo.SessionDeFormations");
            DropTable("dbo.Formations");
            DropTable("dbo.Cursus");
            DropTable("dbo.SessionDeCursus");
            DropTable("dbo.Messages");
            DropTable("dbo.Apprenants");
            DropTable("dbo.Admins");
        }
    }
}

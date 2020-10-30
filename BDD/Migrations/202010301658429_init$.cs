namespace BDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendants",
                c => new
                    {
                        AttendantId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Age = c.String(),
                        Login = c.String(),
                        Mdp = c.String(),
                        AdresseMail = c.String(),
                    })
                .PrimaryKey(t => t.AttendantId);
            
            CreateTable(
                "dbo.CursusSessions",
                c => new
                    {
                        CursusSessionId = c.Int(nullable: false, identity: true),
                        CursusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CursusSessionId)
                .ForeignKey("dbo.Cursus", t => t.CursusId, cascadeDelete: true)
                .Index(t => t.CursusId);
            
            CreateTable(
                "dbo.FormationSessions",
                c => new
                    {
                        FormationSessionId = c.Int(nullable: false, identity: true),
                        FormateurId = c.Int(nullable: false),
                        FormationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormationSessionId)
                .ForeignKey("dbo.Formations", t => t.FormationId, cascadeDelete: true)
                .ForeignKey("dbo.Formateurs", t => t.FormateurId, cascadeDelete: true)
                .Index(t => t.FormateurId)
                .Index(t => t.FormationId);
            
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
                "dbo.Formateurs",
                c => new
                    {
                        FormateurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Age = c.String(),
                        Login = c.String(),
                        Mdp = c.String(),
                        AdresseMail = c.String(),
                    })
                .PrimaryKey(t => t.FormateurId);
            
            CreateTable(
                "dbo.CursusSessionAttendants",
                c => new
                    {
                        CursusSession_CursusSessionId = c.Int(nullable: false),
                        Attendant_AttendantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CursusSession_CursusSessionId, t.Attendant_AttendantId })
                .ForeignKey("dbo.CursusSessions", t => t.CursusSession_CursusSessionId, cascadeDelete: true)
                .ForeignKey("dbo.Attendants", t => t.Attendant_AttendantId, cascadeDelete: true)
                .Index(t => t.CursusSession_CursusSessionId)
                .Index(t => t.Attendant_AttendantId);
            
            CreateTable(
                "dbo.FormationSessionCursusSessions",
                c => new
                    {
                        FormationSession_FormationSessionId = c.Int(nullable: false),
                        CursusSession_CursusSessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FormationSession_FormationSessionId, t.CursusSession_CursusSessionId })
                .ForeignKey("dbo.FormationSessions", t => t.FormationSession_FormationSessionId, cascadeDelete: true)
                .ForeignKey("dbo.CursusSessions", t => t.CursusSession_CursusSessionId, cascadeDelete: true)
                .Index(t => t.FormationSession_FormationSessionId)
                .Index(t => t.CursusSession_CursusSessionId);
            
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
            DropForeignKey("dbo.FormationSessions", "FormateurId", "dbo.Formateurs");
            DropForeignKey("dbo.FormationSessions", "FormationId", "dbo.Formations");
            DropForeignKey("dbo.FormationCursus", "Cursus_CursusId", "dbo.Cursus");
            DropForeignKey("dbo.FormationCursus", "Formation_FormationId", "dbo.Formations");
            DropForeignKey("dbo.CursusSessions", "CursusId", "dbo.Cursus");
            DropForeignKey("dbo.FormationSessionCursusSessions", "CursusSession_CursusSessionId", "dbo.CursusSessions");
            DropForeignKey("dbo.FormationSessionCursusSessions", "FormationSession_FormationSessionId", "dbo.FormationSessions");
            DropForeignKey("dbo.CursusSessionAttendants", "Attendant_AttendantId", "dbo.Attendants");
            DropForeignKey("dbo.CursusSessionAttendants", "CursusSession_CursusSessionId", "dbo.CursusSessions");
            DropIndex("dbo.FormationCursus", new[] { "Cursus_CursusId" });
            DropIndex("dbo.FormationCursus", new[] { "Formation_FormationId" });
            DropIndex("dbo.FormationSessionCursusSessions", new[] { "CursusSession_CursusSessionId" });
            DropIndex("dbo.FormationSessionCursusSessions", new[] { "FormationSession_FormationSessionId" });
            DropIndex("dbo.CursusSessionAttendants", new[] { "Attendant_AttendantId" });
            DropIndex("dbo.CursusSessionAttendants", new[] { "CursusSession_CursusSessionId" });
            DropIndex("dbo.FormationSessions", new[] { "FormationId" });
            DropIndex("dbo.FormationSessions", new[] { "FormateurId" });
            DropIndex("dbo.CursusSessions", new[] { "CursusId" });
            DropTable("dbo.FormationCursus");
            DropTable("dbo.FormationSessionCursusSessions");
            DropTable("dbo.CursusSessionAttendants");
            DropTable("dbo.Formateurs");
            DropTable("dbo.Formations");
            DropTable("dbo.Cursus");
            DropTable("dbo.FormationSessions");
            DropTable("dbo.CursusSessions");
            DropTable("dbo.Attendants");
        }
    }
}

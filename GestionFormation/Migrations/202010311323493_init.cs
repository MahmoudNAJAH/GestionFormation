namespace GestionFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId", "dbo.Formateurs");
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
            
            AddColumn("dbo.Messages", "Chat_ChatId", c => c.Int());
            AddColumn("dbo.SessionDeFormations", "Formateur_FormateurId1", c => c.Int());
            CreateIndex("dbo.Messages", "Chat_ChatId");
            CreateIndex("dbo.SessionDeFormations", "Formateur_FormateurId1");
            AddForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId", "dbo.Formateurs", "FormateurId");
            AddForeignKey("dbo.Messages", "Chat_ChatId", "dbo.Chats", "ChatId");
            AddForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId1", "dbo.Formateurs", "FormateurId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId1", "dbo.Formateurs");
            DropForeignKey("dbo.Chats", "SessionDeCursusId", "dbo.SessionDeCursus");
            DropForeignKey("dbo.Messages", "Chat_ChatId", "dbo.Chats");
            DropForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId", "dbo.Formateurs");
            DropIndex("dbo.Chats", new[] { "SessionDeCursusId" });
            DropIndex("dbo.SessionDeFormations", new[] { "Formateur_FormateurId1" });
            DropIndex("dbo.Messages", new[] { "Chat_ChatId" });
            DropColumn("dbo.SessionDeFormations", "Formateur_FormateurId1");
            DropColumn("dbo.Messages", "Chat_ChatId");
            DropTable("dbo.Chats");
            AddForeignKey("dbo.SessionDeFormations", "Formateur_FormateurId", "dbo.Formateurs", "FormateurId");
        }
    }
}
